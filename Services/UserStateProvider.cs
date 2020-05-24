using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BlazorChat.Services
{
    public interface IUserStateProvider
    {
        void AddOrUpdate(UserState state);
        IEnumerable<UserState> GetAll();
        void Remove(string username);
        UserState UserByClient(ConnectedClient client);
    }

    public class UserStateProvider : IUserStateProvider
    {
        private ConcurrentDictionary<string, UserState> _users;
        public UserStateProvider()
        {
            _users = new ConcurrentDictionary<string, UserState>();
        }

        public void AddOrUpdate(UserState state)
        {
            _users.AddOrUpdate(state.Username, k => state, (k, s) => state);
        }

        public IEnumerable<UserState> GetAll()
        {
            return _users.Values;
        }

        public void Remove(string username)
        {
            _users.TryRemove(username, out var _);
        }

        public UserState UserByClient(ConnectedClient client)
        {
            throw new NotImplementedException();
        }
    }
}
