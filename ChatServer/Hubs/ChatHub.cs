using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, HashSet<string>> _connections = new();

    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId))
        {
            var set = _connections.GetOrAdd(userId, _ => new HashSet<string>());
            lock (set) set.Add(Context.ConnectionId);
            Clients.All.SendAsync("UserStatusChanged", userId, true);
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId) && _connections.TryGetValue(userId, out var set))
        {
            lock (set) set.Remove(Context.ConnectionId);
            if (set.Count == 0)
            {
                _connections.TryRemove(userId, out _);
                Clients.All.SendAsync("UserStatusChanged", userId, false);
            }
        }
        return base.OnDisconnectedAsync(exception);
    }

    public Task JoinConversation(Guid conversationId) => Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
    public Task LeaveConversation(Guid conversationId) => Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
}
