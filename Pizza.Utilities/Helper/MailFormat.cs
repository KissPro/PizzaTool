using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza.Utilities.Helper
{
    public class MailFormat
    {
        public static string webUrl = "http://localhost:55500";

        public static string Email_Noti_Deadline = "Dear Mr/Ms. {0},</br></br>" +
            "You have received a Deadline Notification in Pizza system.</br>" +
            "Step pending: {1}</br>" +
            "Please follow below link to view : <a href='" + webUrl + "/pages/tables/create-issue;issueId={2};type=open;step=openIssue" + "'>Pizza - Open Issue</a></br></br>" +
            "Best regards," +
            "</br><a href='" + webUrl + "'>Pizza System</a></br>";
    }
}
