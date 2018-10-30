using System;
using System.IO;

namespace Atarashii.CLI
{
    /// <inheritdoc />
    /// <summary>
    ///     Type dealing with writing inbound messages to the console.
    /// </summary>
    public class CliOutput : Output
    {
        /// <inheritdoc />
        /// <summary>
        ///     Decorates the inbound message and writes it to the stdout/stderr.
        /// </summary>
        public override void Write(Type type, string subject, string message)
        {
            if (type == Type.Error)
            {
                // indent without altering error message
                Console.Write("  ");
                Console.Error.WriteLine(message);
            }

            var code = CodeFactory.Get(type);

            // write code
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  [ ");
            Console.ForegroundColor = code.Colour;
            Console.Write(code.Value);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" ] ");

            // write subject
            Console.Write("| ");
            Console.Write(subject);
            Console.Write(" | ");

            // write message
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
        }

        /// <summary>
        ///     Outputs the banner to the CLI.
        /// </summary>
        public static void WriteBanner()
        {
            void WriteFromResource(Resource resource)
            {
                using (var reader = new StringReader(resource.Text))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null) Console.WriteLine("  " + line);
                }

                Console.WriteLine(string.Empty);
            }

            void ShowAsciiBanner()
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                WriteFromResource(ResourceFactory.Get(ResourceFactory.Type.Banner));
                Console.WriteLine(string.Empty);
            }

            void ShowProductName()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  Program    : ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  " + Assembly.ProductName);
            }

            void ShowCompanyName()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  Developers : ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  " + Assembly.CompanyName);
            }

            void ShowGitRevision()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  Revision   : ");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  " + Git.Revision);
            }

            void ShowProgramHelp()
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                WriteFromResource(ResourceFactory.Get(ResourceFactory.Type.Usage));
            }

            ShowAsciiBanner();
            ShowProductName();
            ShowCompanyName();
            ShowGitRevision();
            ShowProgramHelp();
        }

        /// <summary>
        ///     Factory for building Code objects.
        /// </summary>
        private static class CodeFactory
        {
            /// <summary>
            ///     Returns a Code object based on the inbound Type.
            /// </summary>
            /// <param name="type">
            ///     Output type. <see cref="Type" />
            /// </param>
            /// <returns>
            ///     Code instance whose properties are determined by the inbound type.
            /// </returns>
            /// <exception cref="ArgumentOutOfRangeException">
            ///     Invalid Type enum value.
            /// </exception>
            public static Code Get(Type type)
            {
                switch (type)
                {
                    case Type.Success:
                        return new Code(" OK ", ConsoleColor.Green);
                    case Type.Info:
                        return new Code("INFO", ConsoleColor.Cyan);
                    case Type.Error:
                        return new Code("HALT", ConsoleColor.Red);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }

        /// <summary>
        ///     Represents a decorative output code.
        /// </summary>
        private class Code
        {
            /// <summary>
            /// </summary>
            /// <param name="value">
            ///     <see cref="Value" />
            /// </param>
            /// <param name="colour">
            ///     <see cref="Colour" />
            /// </param>
            public Code(string value, ConsoleColor colour)
            {
                Value = value;
                Colour = colour;
            }

            /// <summary>
            ///     Code value.
            ///     <example>
            ///         OK, INFO, HALT
            ///     </example>
            /// </summary>
            public string Value { get; }

            /// <summary>
            ///     Colour of the code in the console.
            /// </summary>
            public ConsoleColor Colour { get; }
        }
    }
}