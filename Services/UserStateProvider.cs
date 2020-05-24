using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BlazorChat.Services
{
    public interface IUserStateProvider
    {
        void AddOrUpdate(UserState state);
        IEnumerable<UserState> GetAll();
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

        public IEnumerable<UserState> GetAll()
        {
            return _users.Values;
        }

        public void Remove(Guid id)
        {
            _users.TryRemove(id, out var _);
        }
    }
}
