﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 17.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace QuantitiesDotNet.Generators
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\develop\QuantitiesDotNet\src\QuantitiesDotNet.Generators\AttributesText.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class AttributesText : AttributesTextBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\n\r\nnamespace QuantitiesDotNet;\r\n\r\n\r\n[AttributeUsage(AttributeTargets.S" +
                    "truct, AllowMultiple = false)]\r\ninternal sealed class QuantityAttribute : Attrib" +
                    "ute\r\n{\r\n    public int L { get; }\r\n    public int M { get; }\r\n    public int T {" +
                    " get; }\r\n    public int I { get; }\r\n    public int Th { get; }\r\n    public int N" +
                    " { get; }\r\n    public int J { get; }\r\n\r\n    public QuantityAttribute(int L, int " +
                    "M, int T, int I, int Th, int N, int J)\r\n    {\r\n        this.L  = L ;\r\n        th" +
                    "is.M  = M ;\r\n        this.T  = T ;\r\n        this.I  = I ;\r\n        this.Th = Th;" +
                    "\r\n        this.N  = N ;\r\n        this.J  = J ;\r\n    }\r\n}\r\n\r\n[AttributeUsage(Attr" +
                    "ibuteTargets.Struct, AllowMultiple = true)]\r\ninternal sealed class QuantityUnitA" +
                    "ttribute : Attribute\r\n{\r\n    public string Name { get; }\r\n    public string Unit" +
                    " { get; }\r\n    public double Scale { get; }\r\n    public UnitPrefix Prefix { get;" +
                    " }\r\n    public int PowerOfPrefix { get; }\r\n    public bool ExportsShorthandSymbo" +
                    "l { get; }\r\n    \r\n    /// <summary>\r\n    /// Creates a new instance of <see cref" +
                    "=\"QuantityUnitAttribute\"/>.\r\n    /// </summary>\r\n    /// <param name=\"name\"> The" +
                    " name of unit. </param>\r\n    /// <param name=\"unit\"> The simply unit symbol expr" +
                    "ession in ASCII. </param>\r\n    /// <param name=\"scale\">\r\n    ///     <para>The s" +
                    "cale of the unit from raw value.</para>\r\n    ///     <para>e.g.: `scale` for \"gr" +
                    "am\" is 1e-3, because raw value is killogram</para>\r\n    /// </param>\r\n    /// <p" +
                    "aram name=\"prefix\"> The set of available SI prefix. </param>\r\n    /// <param nam" +
                    "e=\"powerOfPrefix\">\r\n    ///     <para>The coefficient for SI prefix exponent.</p" +
                    "ara>\r\n    ///     <para>\r\n    ///         e.g.\r\n    ///         <list type=\"tabl" +
                    "e\">\r\n    ///             <item>\r\n    ///                 <term>for <see cref=\"QS" +
                    "patialFrequency\" /></term>\r\n    ///                 <description>-1</description" +
                    ">\r\n    ///             </item>\r\n    ///             <item>\r\n    ///             " +
                    "    <term>for <see cref=\"QLength\" /></term>\r\n    ///                 <descriptio" +
                    "n>1</description>\r\n    ///             </item>\r\n    ///             <item>\r\n    " +
                    "///                 <term>for <see cref=\"QArea\" /></term>\r\n    ///              " +
                    "   <description>2</description>\r\n    ///             </item>\r\n    ///           " +
                    "  <item>\r\n    ///                 <term>for <see cref=\"QVolume\" /></term>\r\n    /" +
                    "//                 <description>3</description>\r\n    ///             </item>\r\n  " +
                    "  ///         </list>\r\n    ///     </para>\r\n    /// </param>\r\n    /// <param nam" +
                    "e=\"exportsShorthandSymbol\">\r\n    ///     <para>If <c>true</c> generates symbol f" +
                    "ield for unit shorthands into <see cref=\"QuantitiesDotNet.UnitShorthands.UnitExtens" +
                    "ions\" />:</para>\r\n    ///     <para>otherwise <c>false</c>.</para>\r\n    /// </pa" +
                    "ram>\r\n    public QuantityUnitAttribute(\r\n        string name,\r\n        string un" +
                    "it,\r\n        double scale,\r\n        UnitPrefix prefix = UnitPrefix.None,\r\n      " +
                    "  int powerOfPrefix = 1,\r\n        bool exportsShorthandSymbol = false)\r\n    {\r\n " +
                    "       Name = name;\r\n        Unit = unit;\r\n        Scale = scale;\r\n        Prefi" +
                    "x = prefix;\r\n        PowerOfPrefix = powerOfPrefix;\r\n        ExportsShorthandSym" +
                    "bol = exportsShorthandSymbol;\r\n    }\r\n}\r\n\r\n\r\n[AttributeUsage(AttributeTargets.St" +
                    "ruct, AllowMultiple = true)]\r\ninternal sealed class QuantityOperationAttribute :" +
                    " Attribute\r\n{\r\n    public Type MultiplicantType { get; }\r\n    public Type Multip" +
                    "lierType { get; }\r\n    public Type ProductType { get; }\r\n\r\n    public QuantityOp" +
                    "erationAttribute(Type multiplicant, Type multiplier, Type product)\r\n    {\r\n     " +
                    "   MultiplicantType = multiplicant;\r\n        MultiplierType = multiplier;\r\n     " +
                    "   ProductType = product;\r\n    }\r\n}\r\n\r\n\r\n[Flags]\r\ninternal enum UnitPrefix\r\n{\r\n " +
                    "   None = 0,\r\n\r\n    Centi  = 1 << 0,\r\n    Milli  = 1 << 1,\r\n    Micro  = 1 << 2," +
                    "\r\n    Nano   = 1 << 3,\r\n    Pico   = 1 << 4,\r\n    Femto  = 1 << 5,\r\n    Atto   =" +
                    " 1 << 6,\r\n    Zepto  = 1 << 7,\r\n    Yocto  = 1 << 8,\r\n    Ronto  = 1 << 9,\r\n    " +
                    "Quecto = 1 << 10,\r\n\r\n    Hecto  = 1 << (0 + 16),\r\n    Kilo   = 1 << (1 + 16),\r\n " +
                    "   Mega   = 1 << (2 + 16),\r\n    Giga   = 1 << (3 + 16),\r\n    Tera   = 1 << (4 + " +
                    "16),\r\n    Peta   = 1 << (5 + 16),\r\n    Exa    = 1 << (6 + 16),\r\n    Zetta  = 1 <" +
                    "< (7 + 16),\r\n    Yotta  = 1 << (8 + 16),\r\n    Ronna  = 1 << (9 + 16),\r\n    Quett" +
                    "a = 1 << (10 + 16),\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class AttributesTextBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        public System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
