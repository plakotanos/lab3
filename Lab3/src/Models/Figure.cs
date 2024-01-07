using System.ComponentModel.DataAnnotations;

namespace Lab3.Models;

using System.Text;
using System.Xml.Serialization;

[XmlRoot(ElementName = "Figure", IsNullable = false)]
public class Figure
{
	[Key]
	public int Id { get; set; }
	
	[XmlArray("Steps")]
	public List<Step> Steps { get; set; } = new();

	public Figure(List<Step> steps)
	{
		Steps = new(steps);
	}

	public Figure()
	{}

	public override string ToString()
	{
		StringBuilder builder = new("Figure with lines:\n");

		foreach (Step step in Steps)
		{
			builder.AppendLine(step.ToString());
		}

		return builder.ToString();
	}
}
