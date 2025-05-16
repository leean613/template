namespace DTOs.Share
{
    public class SendEmailInput
    {
        public string Subject { get; set; }

        public string[] RecipientEmails { get; set; }

        public string[] CcEmails { get; set; }

        public string[] BccEmails { get; set; }

        public string Content { get; set; }

        public FileDescription[] AttachmentFiles { get; set; }
    }
}
