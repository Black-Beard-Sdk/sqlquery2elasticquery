using Bb.Elastic.Runtimes;

namespace Bb.Elastic.SqlParser.Models
{

    [System.Diagnostics.DebuggerDisplay("{Value}")]
    public class Literal : AstBase
    {


        public Literal(Locator position) : base(position)
        {

        }
        public object Value { get; set; }


        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }

    }

}
