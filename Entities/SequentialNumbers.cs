using System.ComponentModel.DataAnnotations.Schema;

namespace Deli.Entities;
public class SequentialNumbers
{
    public Guid Id { get; set; }

    public int LastRefNumber { get; set; } = 99999;
    public long LastOrderNumber { get; set; } = 0;
}