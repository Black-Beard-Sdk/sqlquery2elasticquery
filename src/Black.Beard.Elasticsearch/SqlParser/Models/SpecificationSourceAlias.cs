using Bb.Elastic.Runtimes;

namespace Bb.Elastic.SqlParser.Models
{
    public class SpecificationSourceAlias : SpecificationSource
    {

        public SpecificationSourceAlias(Locator position) : base(position)
        {

        }

        public AliasReferenceAst Alias { get; set; }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.SpecificationSourceAlias(this);
        }

    }


}
