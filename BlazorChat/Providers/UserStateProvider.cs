using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BlazorChat.Models;

namespace BlazorChat.Providers
{
    public interface IUserStateProvider
    {
        void AddOrUpdate(User state);
        IEnumerable<User> GetAll();
        void Remove(string username);
        User GetByClient(ConnectedClient client);
        User GetByUsername(string username);
    }

    public class UserStateProvider : IUserStateProvider
    {
        private ConcurrentDictionary<string, User> _users;
        private ConcurrentDictionary<string, User> _usersByClientId;
        public UserStateProvider()
        {
            _users = new ConcurrentDictionary<string, User>();
            _usersByClientId = new ConcurrentDictionary<string, User>();
        }

        public void AddOrUpdate(User state)
        {
            _users.AddOrUpdate(state.Username, k => state, (k, s) => state);
            _usersByClientId.AddOrUpdate(state.Client.Id, k => state, (k, s) => state);
        }

        public User GetByUsername(string username)
        {
            return _users.ContainsKey(username) ? _users[username] : null;
        }

        public User GetByClient(ConnectedClient client)
        {
            return _usersByClientId.ContainsKey(client.Id) ? _usersByClientId[client.Id] : null;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Values;
        }

        public void Remove(string username)
        {
            _users.TryRemove(username, out var _);
        }
    }
}
