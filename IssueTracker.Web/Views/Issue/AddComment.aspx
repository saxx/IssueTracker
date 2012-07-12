<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IssueTracker.Models.Issue>" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <div style="padding: 5px; width: 500px; margin-left: auto; margin-right: auto;">
        <form method="post" action="<%= Url.Action("AddComment", "Issue", new { id = Model.Id }) %>">
        <table cellpadding="3" cellspacing="0">
            <tr>
                <td>
                    Status:
                </td>
                <td style="padding-left: 7px;">
                    <select name="status">
                        <% foreach (string status in (ViewData["availableStati"] as IEnumerable<string>)) { %>
                        <option value="<%= status %>" <%= status.Equals(Model.Status, StringComparison.InvariantCultureIgnoreCase) ? " selected=\"selected\"" : "" %>>
                            <%= status %></option>
                        <% } %>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Comment:
                </td>
                <td style="padding-left: 7px;">
                    <%= Html.TextArea("comment", new { style = "width:375px; height:100px;" }) %>
                </td>
            </tr>
            <tr>
                <td>
                    Notification to:
                </td>
                <td style="padding-left: 7px;">
                    <%= Html.TextBox("email", null, new { style = "width:375px;" }) %>
                </td>
            </tr>
            <tr>
                <td>
                    Make public:
                </td>
                <td>
                    <%= Html.CheckBox("public", Model.IsPublic) %>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" value="Save" />
                </td>
            </tr>
        </table>
        </form>
    </div>

    <script type="text/javascript">
        $("#email").keyup(function() {
            $("#public").attr("checked", $("#email").val() == "" ? "" : "checked");
        });
    </script>

</asp:Content>