
using Bb.Elastic.Runtimes;

namespace Bb.Elastic.SqlParser.Models
{

    [System.Diagnostics.DebuggerDisplay("{Rule}")]
    public class SpecificationFilter : AstBase
    {


        public SpecificationFilter(Locator position) : base(position)
        {

        }

        public AstBase Rule { get; set; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitSpecificationFilter(this);
        }

    }

}
