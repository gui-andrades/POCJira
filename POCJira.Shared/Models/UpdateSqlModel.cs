using System.Linq;
using System;
using System.Collections.Generic;

namespace POCJira.Shared.Models
{
    public class UpdateSqlModel
    {
        public long Timestamp { get; set; }

        public String WebhookEvent { get; set; }

        public String Issue_Event_Type_Name { get; set; }

        public UserModel User { get; set; }

        public IssueModel Issue { get; set; }

        public ChangelogModel Changelog { get; set; }

        public ChangelogItemModel StatusChange
        {
            get => Changelog.Items.FirstOrDefault((item) => item.Field == "status");
        }
    }

    public class UserModel
    {
        public String Self { get; set; }

        public String AccountId { get; set; }

        public String DisplayName { get; set; }
    }

    public class IssueModel
    {
        public String Id { get; set; }

        public String Self { get; set; }

        public String Key { get; set; }

        public FieldsModel Fields { get; set; }
    }

    public class FieldsModel
    {
        public String StatusCategoryChangeDate { get; set; }

        public String Updated { get; set; }

        public String Summary { get; set; }

        public String Description { get; set; }

        public StatusModel Status { get; set; }

        public UserModel Assignee { get; set; }

        public float? CustomField_10030 { get; set; }
    }

    public class StatusModel
    {
        public String Name { get; set; }
    }

    public class ChangelogModel
    {
        public String Id { get; set; }

        public IEnumerable<ChangelogItemModel> Items { get; set; }
    }

    public class ChangelogItemModel
    {
        public String Field { get; set; }

        public String FieldType { get; set; }

        public String FieldId { get; set; }

        public String From { get; set; }

        public String FromString { get; set; }

        public String To { get; set; }

        new public String ToString { get; set; }
    }
}
