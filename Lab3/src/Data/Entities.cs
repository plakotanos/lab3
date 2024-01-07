namespace Lab3.Data;

using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TurtleEntity
{
    [Key]
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Direction { get; set; }
    public bool PenIsDown { get; set; }
    public PenColor penColor { get; set; }

    [ForeignKey("TurtleStepId")]
    public List<StepEntity> Steps { get; } = new();
    [ForeignKey("TurtlePathId")]
    public List<StepEntity> Path { get; } = new();
    public List<FigureEntity> Figures { get; } = new();
}

public class StepEntity
{
    [Key]
    public int Id { get; set; }
	public int StartX { get; set; }
	public int StartY { get; set; }
	public int EndX { get; set; }
	public int EndY { get; set; }
	public bool PenDown { get; set; }
	public PenColor Color { get; set; }

    public int TurtleStepId { get; set; }
    public int TurtlePathId { get; set; }

    public int FigureId { get; set;}
}

public class FigureEntity
{
    [Key]
    public int Id { get; set; }

    public List<StepEntity> Steps { get; } = new();
}
