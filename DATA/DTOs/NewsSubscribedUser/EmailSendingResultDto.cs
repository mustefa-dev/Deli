namespace Deli.DATA.DTOs;

public class EmailSendingResultDto
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public int NumOfUsersSentTo { get; set; }
}