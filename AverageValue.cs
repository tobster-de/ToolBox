
namespace ToolBox
{
    /// <summary>
    /// This class automatically represents an average value.
    /// </summary>
    /// <example>
    /// <code>
    /// AverageValue average = new AverageValue();
    /// //add some values
    /// for (int i = 0; i &lt; 10; i++)
    /// {
    ///     average.Add(i);
    /// }
    /// //implicit cast
    /// Console.Writeln(String.Format("Average: {0:f}", average));
    /// //reset the value
    /// average.Reset();
    /// </code>
    /// </example>
    public class AverageValue
    {
        #region Fields

        private int m_ValueCount;
        private double m_Value;

        #endregion

        #region Properties

        /// <summary>
        /// average value of this object
        /// </summary>
        public double Value
        {
            get
            {
                return m_Value / m_ValueCount;
            }
        }

        #endregion

        #region Construction

        public AverageValue()
        {
            Reset();
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implicit cast oparator
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static implicit operator double(AverageValue a)
        {
            return a.Value;
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// adds a value to the average
        /// </summary>
        /// <param name="value">value to add</param>
        public void Add(double value)
        {
            m_ValueCount++;
            m_Value += value;
        }

        /// <summary>
        /// Resets this value;
        /// </summary>
        public void Reset()
        {
            m_Value = 0;
            m_ValueCount = 0;
        }

        #endregion
    }
}
