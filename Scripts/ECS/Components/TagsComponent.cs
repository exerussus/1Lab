
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Tags")]
    public class TagsComponent : OneLabComponent
    {
        [SerializeField] private string[] values;

        public string[] Values
        {
            get => values;
            set => values = value;
        }

        public override void Initialize()
        {
            ref var tagsData = ref Componenter.AddOrGet<TagsData>(Entity);
            tagsData.Tags = values;
        }

        public override void Destroy()
        {
            Componenter.Del<TagsData>(Entity);
        }
    }

    public struct TagsData : IEcsComponent
    {
        public string[] Tags;
    }
}