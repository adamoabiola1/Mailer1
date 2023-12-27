namespace C__Mailer.Models
{
    public class EmailEntity
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string EmailBodyMessage { get; set; }

        [Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
        public string AttachmentURL { get; set; }
    }
}

