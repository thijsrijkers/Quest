using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Quest.Minecraft
{
    public interface IEntity
    {
        /// <summary>
        /// The Minecraft entity type.
        /// </summary>
        EntityType Type { get; }

        int? id { get; }
    }
}
