using System;

namespace Quest.Minecraft.Events
{
    /// <summary>
    /// Info about chat.
    /// </summary>
    public class ChatEvent : EventArgs
    {
        public ChatEvent(int entityId, string message) : base()
        {
            EntityId = entityId;
            Message = message;
        }

        /// <summary>
        /// Get entityID from sender
        /// </summary>
        public int EntityId { get; }

        /// <summary>
        /// The message that was sent.
        /// </summary>
        public string Message { get; }
    }
}
