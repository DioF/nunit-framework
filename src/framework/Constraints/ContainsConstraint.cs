// ***********************************************************************
// Copyright (c) 2007 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************


namespace NUnit.Framework.Constraints
{
    // TODO Needs tests
    /// <summary>
    /// ContainsConstraint tests a whether a string contains a substring
    /// or a collection contains an object. It postpones the decision of
    /// which test to use until the type of the actual argument is known.
    /// This allows testing whether a string is contained in a collection
    /// or as a substring of another string using the same syntax.
    /// </summary>
    public class ContainsConstraint : Constraint
    {
        readonly object expected;
        Constraint realConstraint;
        bool ignoreCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public ContainsConstraint(object expected)
        {
            this.expected = expected;
        }

        /// <summary>
        /// The Description of what this constraint tests, for
        /// use in messages and in the ConstraintResult.
        /// </summary>
        public override string Description
        {
            get { return this.realConstraint.Description; }
        }

        /// <summary>
        /// Flag the constraint to ignore case and return self.
        /// </summary>
        public ContainsConstraint IgnoreCase
        {
            get { this.ignoreCase = true; return this; }
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (actual is string)
            {
                StringConstraint constraint = new SubstringConstraint((string)expected);
                if (this.ignoreCase)
                    constraint = constraint.IgnoreCase;
                this.realConstraint = constraint;
            }
            else
                this.realConstraint = new CollectionContainsConstraint(expected);

            return this.realConstraint.ApplyTo(actual);
        }
    }
}
