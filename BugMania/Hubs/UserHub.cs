using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using BugMania.Shapes;

namespace BugMania.Hubs
{
    [HubName("UserHub")]
    public class UserHub : Hub
    {
        [HubMethodName("UpdateSubscribers")]
        public static void NotifyBugReportChangeToSubscribers(IList<string> usersId, int id, string title, string operation)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.Users(usersId).UpdateSubscribers(id, title, operation);
        }
    }
}