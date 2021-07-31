using Bb.Elastic.Runtimes;

namespace Bb.Elastic.SqlParser.Models
{

    [System.Diagnostics.DebuggerDisplay("{Reference} AS {AliasName}")]
    public class AliasReferenceAst : Identifier
    {

        public AliasReferenceAst(Locator position) : base(position)
        {

        }

        public Identifier AliasName { get; set; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.VisitAlias(this);
        }

    }


}
