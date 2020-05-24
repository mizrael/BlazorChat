using System;
using System.Collections.Concurrent;

namespace BlazorChat.Services
{
    public interface IUserStateProvider
    {
        void AddOrUpdate(UserState state);
        UserState Get(Guid id);
        void Remove(Guid id);
    }

    public class UserStateProvider : IUserStateProvider
    {
        private ConcurrentDictionary<Guid, UserState> _users;
        public UserStateProvider()
        {
            _users = new ConcurrentDictionary<Guid, UserState>();
        }

        public void AddOrUpdate(UserState state)
        {
            _users.AddOrUpdate(state.Id, k => state, (k, s) => state);
        }

        public UserState Get(Guid id)
        {
            return _users[id];
        }

        public void Remove(Guid id)
        {
            _users.TryRemove(id, out var _);
        }
    }
}
