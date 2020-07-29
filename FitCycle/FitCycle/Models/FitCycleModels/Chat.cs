using FitCycle.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FitCycle.Models
{
    public class Chat
    {
        MainPage mainPage { get => Application.Current.MainPage as MainPage; }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Dictionary<int,User> UserList { get; private set; }
        public ObservableCollection<ChatMessage> MessageList { get; private set; }
        public bool HasNewMessage
        {
            get
            {
                var list = MessageList.Where(n => n.ReadTime == DateTime.MinValue && n.IsfromOtherUser);
                return (list?.Count() > 0);
            }
        }
        public ChatMessage LastMessage
        { 
            get
            {
                Sort();
                if (MessageList.Count > 0)
                    return MessageList.Last();
                return null;
            }
        }
        public int NewMessageCount
        {
            get
            {
                var list = MessageList.Where(n => n.ReadTime == DateTime.MinValue && n.IsfromOtherUser);
                if (list?.Count() > 0)
                    return list.Count();
                return 0;
            }
        }
        public Chat(int id, Dictionary<int, User> userList)
        {
            ID = id;
            UserList = userList;
            Name = userList.Where(n => n.Key != mainPage.User.UserID).First().Value.UserName;
            MessageList = new ObservableCollection<ChatMessage>();
        }

        public string GetUserListIDsString()
        {
            var userListIDsString = string.Empty;
            foreach (var id in UserList.Keys.Where(n => n != mainPage.User.UserID))
            {
                userListIDsString += "," + id;
            }
            userListIDsString += ",";
            return userListIDsString;
        }

        public void Sort()
        {
            var oldlist = MessageList.ToList();
            if (oldlist.Count >0)
                oldlist.Sort(delegate (ChatMessage m1, ChatMessage m2) { return m1.SendTime.CompareTo(m2.SendTime); });
            var newlist = new ObservableCollection<ChatMessage>();
            foreach (var element in oldlist)
            {
                newlist.Add(element);
            }
            MessageList = newlist;
        }
    }
}
