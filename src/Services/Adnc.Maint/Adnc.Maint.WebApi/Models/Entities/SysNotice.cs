namespace Adnc.Maint.Entities;

/// <summary>
/// 通知
/// </summary>
public class SysNotice : Entity
{
    public string Content { get; set; }

    public string Title { get; set; }

    public int? Type { get; set; }
}