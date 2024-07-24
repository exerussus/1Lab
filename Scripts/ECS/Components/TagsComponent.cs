
using OneLab.Scripts.ECS.Core.Interfaces;
using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Tags")]
    public class TagsComponent : EcsComponent
    {
        [FormerlySerializedAs("tags")] [SerializeField] private string[] values;

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