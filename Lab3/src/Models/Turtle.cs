using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lab3.Models;

using Lab3.Data;
using System.Xml.Serialization;

public enum PenColor { Black, Green }

public delegate IDbContextTransaction? TransactionSupplier();

static class PenColorExtensions
{
	private static readonly Dictionary<string, PenColor> StrToColor = new()
	{
		{ "black", PenColor.Black },
		{ "green", PenColor.Green }
	};

	public static PenColor? ToPenColor(this string color)
	{
		if (StrToColor.TryGetValue(color.ToLower(), out var result))
		{
			return result;
		}

		return null;
	}
}

[XmlRoot(ElementName = "Turtle", IsNullable = false)]
public class Turtle : ILoadable<Turtle>
{
	[Key]
	public int? Id { get; set; }
	
	public int X { get; set; }
	public int Y { get; set; }
	public int Direction { get; set; }
	public bool PenIsDown { get; set; }

	[XmlArray("Steps")]
	[ForeignKey("TurtleStepId")]
	public List<Step> Steps { get; set; } = new();

	[XmlArray("Path")]
	[ForeignKey("TurtlePathId")]
	public List<Step> Path { get; set; } = new();

	[XmlArray("Items")]
	public List<Figure> Figures { get; set; } = new();

	public PenColor Color { get; set; } = PenColor.Black;

	public static readonly PenColor[] PossibleColors = {
		PenColor.Black,
		PenColor.Green
	};

	private TurtleContext PrepareContext()
	{
		var context = new TurtleContext();
		if (Id == null)
		{
			context.Add(this);
		}
		else
		{
			context.Attach(this);
		}

		return context;
	}

	public void Move(int stepsCount)
	{
		var context = PrepareContext();
		
		int newX = X + (int)(stepsCount * Math.Cos(Direction * Math.PI / 180));
		int newY = Y + (int)(stepsCount * Math.Sin(Direction * Math.PI / 180));

		Step step = new(X, Y, newX, newY, PenIsDown, Color);
		Steps.Add(step);

		if (PenIsDown)
		{
			Path.Add(step);
		}

		X = newX;
		Y = newY;

		if (PathIsClosed(step))
		{
			Figure newFigure = new(Path);
			Figures.Add(newFigure);
			Path.Clear();
		}

		context.SaveChanges();
	}

	public void Turn(int angle)
	{
		var context = PrepareContext();
		
		Direction = angle % 360;

		context.SaveChanges();
	}

	public void PenDown()
	{
		var context = PrepareContext();
		
		PenIsDown = true;

		context.SaveChanges();
	}

	public void PenUp()
	{
		var context = PrepareContext();
		
		Path.Clear();
		PenIsDown = false;

		context.SaveChanges();
	}

	public List<(int, int)> GetSnapshot()
	{
		List<(int, int)> snapshot = new();
		foreach (Step step in Steps)
		{
			if (step.PenDown)
			{
				snapshot.Add((step.StartX, step.StartY));
			}
		}
		return snapshot;
	}

	private bool PathIsClosed(Step step)
	{
		for (int i = 0; i < Path.Count - 1; i++)
		{
			int x1 = Path[i].StartX;
			int y1 = Path[i].StartY;
			int x2 = Path[i].EndX;
			int y2 = Path[i].EndY;

			if (LinesCross(x1, y1, x2, y2, step.StartX, step.StartY, step.EndX, step.EndY))
			{
				return true;
			}
		}
		return false;
	}

	private static bool LinesCross(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
	{
		// Вычисляем векторы направления для обеих линий.
		int dx1 = x2 - x1;
		int dy1 = y2 - y1;
		int dx2 = x4 - x3;
		int dy2 = y4 - y3;

		// Вычисляем вектор между началом первой линии и началом второй линии.
		int dx3 = x1 - x3;
		int dy3 = y1 - y3;

		// Вычисляем определитель матрицы для системы линейных уравнений.
		int determinant = dx1 * dy2 - dx2 * dy1;

		// Проверяем, являются ли линии параллельными (определитель равен 0).
		if (determinant == 0)
		{
			return false;
		}

		// Вычисляем параметры t1 и t2 для точек пересечения.
		double t1 = (dx2 * dy3 - dx3 * dy2) / (double)determinant;
		double t2 = (dx1 * dy3 - dx3 * dy1) / (double)determinant;

		// Проверяем, лежат ли точки пересечения внутри отрезков.
		if (t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1)
		{
			if (x2 == x3 && y2 == y3) return false;
			return true; // Прямые пересекаются.
		}

		return false; // Прямые не пересекаются.
	}

	public void PrintSteps()
	{
		Console.WriteLine("List of executed steps:\n");
		foreach (Step step in Steps)
		{
			Console.WriteLine(step);
		}
	}

	public void PrintFigures()
	{
		Console.WriteLine("List of completed figures:\n");
		foreach (Figure figure in Figures)
		{
			Console.WriteLine(figure);
		}
	}

	public override string ToString()
	{
		string penState = PenIsDown ? "put down" : "put up";
		return $"Current color: {Color}, pen state: {penState}, location ({X}; {Y}), direction: {Direction} degrees.";
	}

	public void PrintState()
	{
		Console.WriteLine(ToString());
	}

	public void LoadFrom(Turtle other)
	{
		X = other.X;
		Y = other.Y;
		Direction = other.Direction;
		PenIsDown = other.PenIsDown;
		Steps = new List<Step>(other.Steps);
		Path = new List<Step>(other.Path);
		Color = other.Color;
		Figures = new List<Figure>(other.Figures);

		if (other.Id != null)
		{
			Id = other.Id;
			return;
		}
		
		var context = new TurtleContext();
		context.Add(this);
		context.SaveChanges();
	}
}
