using System.Collections.Generic;

namespace Plugins.Exerussus._1Lab.Scripts.ECS.Core
{
    public class TagsHandler
    {
        private Dictionary<string, TagPool> _pools = new();
        private Dictionary<int, Tags> _tags = new();
        private Queue<TagPool> _freePools = new();
        private Queue<Tags> _freeTags = new();

        public void Add(string tag, int entity)
        {
            var tags = _tags.TryGetValue(entity, out var result) ? result : GetTags();
            tags.Values.Add(tag);
            
            var pool = GetPool(tag);
            pool.Entities.Add(entity);
        }

        public void Remove(int entity)
        {
            if (!_tags.TryGetValue(entity, out var result)) return;
            
            foreach (var tag in result.Values)
            {
                if (_pools.TryGetValue(tag, out var tagPool))
                {
                    tagPool.Entities.Remove(entity);
                    if (tagPool.Entities.Count == 0)
                    {
                        _pools.Remove(tagPool.TagID);
                        _freePools.Enqueue(tagPool);
                    }
                }
            }
                
            result.Values.Clear();
            _tags.Remove(entity);
            _freeTags.Enqueue(result);
        }

        public bool Has(int entity, string tag)
        {
            if (!_tags.TryGetValue(entity, out var tags)) return false;
            return tags.Values.Contains(tag);
        }

        private Tags GetTags()
        {
            if (_freeTags.TryDequeue(out Tags result))
            {
                result.Values.Clear();
                return result;
            }

            return new Tags();
        }
        
        private TagPool GetPool(string tag)
        {
            if (_pools.TryGetValue(tag, out var value)) return value;
            
            if (_freePools.TryDequeue(out TagPool result))
            {
                result.TagID = tag;
                return result;
            }
            
            var pool = new TagPool { TagID = tag };

            _pools[tag] = pool;
            return pool;
        }
        
        private class TagPool
        {
            public string TagID;
            public HashSet<int> Entities;
        }

        private class Tags
        {
            public HashSet<string> Values;
        }
    }
}