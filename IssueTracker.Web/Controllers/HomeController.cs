﻿using System;
using System.Linq;
using System.Web.Mvc;
using IssueTracker.Web.Models;

namespace IssueTracker.Web.Controllers {

    [Authorize]
    public class HomeController : Controller {

        public ActionResult Index() {
            return RedirectToAction("Index", "Issue");
        }


        [HttpPost, ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Index(int? parentId, string text, string stackTrace, string serverVariables, string source, string version) {
            if (!string.IsNullOrWhiteSpace(version))
                serverVariables = "Version: " + version + Environment.NewLine + (serverVariables ?? "");

            Issue parentIssue = null;
            if (parentId.HasValue)
                using (var context = new Db())
                    parentIssue = context.Issues.FirstOrDefault(x => x.Id == parentId.Value);

            if (parentIssue != null) {
                parentIssue.AddComment(source, text);
                return new JsonResult {
                    Data = parentIssue.Id
                };
            }

            var issue = Issue.Create(source, text, stackTrace, serverVariables);
            return new JsonResult {            
                Data = issue == null ? 0 : issue.Id
            };
        }

    }
}
