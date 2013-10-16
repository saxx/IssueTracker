﻿using ePunkt.Utilities;
using IssueTracker.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IssueTracker.Web.Models.ViewModels.Issue {
    public class DetailsIssuePartialViewModel : IssuePartialViewModel {

        public DetailsIssuePartialViewModel(Db db, User currentUser, Models.Issue issue, ViewDataDictionary viewData)
            : base(issue) {

            CurrentUser = currentUser;

            DateOfCreation = issue.DateOfCreation;
            ParentIssueId = issue.ParentIssueId;
            StackTrace = issue.StackTrace;
            ServerVariables = issue.ServerVariables;
            Comments = (from x in db.Comments
                        where x.IssueId == issue.Id
                        orderby x.DateOfCreation
                        select x).ToList().Select(x => new DetailsCommentPartialViewModel(db, x, viewData));

            var stati = new List<SelectListItem>();
            stati.AddRange(
                    from x in Utils.GetAllStati(db, viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = Status.Is(x.Name)
                    }
                );
            AvailableStati = stati;

            var users = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "< none >",
                    Value = "",
                    Selected = AssignedTo.IsNoE()
                }
            };
            users.AddRange(
                    from x in Utils.GetAllUsers(db, viewData)
                    select new SelectListItem {
                        Text = x.Name,
                        Value = x.Name,
                        Selected = AssignedTo.Is(x.Name)
                    }
                );
            AvailableUsers = users;
        }


        public DateTime DateOfCreation { get; set; }
        public int? ParentIssueId { get; set; }
        public IEnumerable<DetailsCommentPartialViewModel> Comments { get; set; }
        public string StackTrace { get; set; }
        public string ServerVariables { get; set; }

        public IEnumerable<SelectListItem> AvailableStati { get; private set; }
        public IEnumerable<SelectListItem> AvailableUsers { get; private set; }
        public User CurrentUser { get; set; }

    }
}
