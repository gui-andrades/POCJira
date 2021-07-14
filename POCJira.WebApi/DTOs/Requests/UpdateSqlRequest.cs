using System;
using System.Collections.Generic;

namespace POCJira.WebApi.DTOs.Requests
{
    public class UpdateSqlRequest
    {
        public long Timestamp { get; set; }

        public String WebhookEvent { get; set; }

        public String Issue_Event_Type_Name { get; set; }

        public UserRequest User { get; set; }

        public IssueRequest Issue { get; set; }

        public ChangelogRequest Changelog { get; set; }
    }

    public class UserRequest
    {
        public String Self { get; set; }

        public String AccountId { get; set; }

        public String DisplayName { get; set; }
    }

    public class IssueRequest
    {
        public String Id { get; set; }

        public String Self { get; set; }

        public String Key { get; set; }

        public FieldsRequest Fields { get; set; }
    }

    public class FieldsRequest
    {
        public String StatusCategoryChangeDate { get; set; }

        public String Updated { get; set; }

        public String Summary { get; set; }

        public String Description { get; set; }

        public StatusRequest Status { get; set; }

        public UserRequest Assignee { get; set; }

        public float? CustomField_10030 { get; set; }
    }

    public class StatusRequest
    {
        public String Name { get; set; }
    }

    public class ChangelogRequest
    {
        public String Id { get; set; }

        public IEnumerable<ChangelogItemRequest> Items { get; set; }
    }

    public class ChangelogItemRequest
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
