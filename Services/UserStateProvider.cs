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
        UserState GetByClient(ConnectedClient client);
        UserState GetByUsername(string username);
    }

    public class UserStateProvider : IUserStateProvider
    {
        private ConcurrentDictionary<string, UserState> _users;
        private ConcurrentDictionary<string, UserState> _usersByClientId;
        public UserStateProvider()
        {
            _users = new ConcurrentDictionary<string, UserState>();
            _usersByClientId = new ConcurrentDictionary<string, UserState>();
        }

        public void AddOrUpdate(UserState state)
        {
            _users.AddOrUpdate(state.Username, k => state, (k, s) => state);
            _usersByClientId.AddOrUpdate(state.Client.Id, k => state, (k, s) => state);
        }

        public UserState GetByUsername(string username)
        {
            return _users.ContainsKey(username) ? _users[username] : null;
        }

        public UserState GetByClient(ConnectedClient client)
        {
            return _usersByClientId.ContainsKey(client.Id) ? _usersByClientId[client.Id] : null;
        }

        public IEnumerable<UserState> GetAll()
        {
            return _users.Values;
        }

        public void Remove(string username)
        {
            _users.TryRemove(username, out var _);
        }
    }
}
