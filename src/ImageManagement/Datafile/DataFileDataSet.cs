using System;
using System.Xml;

namespace ShatteredTemple.GameLibrarian.ImageManagement.Datafile
{
    public partial class DatafileDataSet
    {
        /// <summary>
        /// Factory method to instantiate an <see cref="DatafileDataSet"/>
        /// from the specified XML file.
        /// </summary>
        /// <remarks>
        /// This needs to be a factory method instead of a constructor, because
        /// <see cref="System.Data.DataSet.ReadXml(string)"/> cannot be called
        /// from a constructor.
        /// </remarks>
        public static DatafileDataSet Create(string filePath)
        {
            // Initialize the result.
            var ds = new DatafileDataSet();

            // Configure the XmlReader so that if the input file has no
            // namespace, we treat it as the one used by our XSD.
            //
            // There *ought* to be a better way to do this, but I haven't found
            // one that works quite yet.
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore,
                NameTable = new NameTable()
            };
            var xmlns = new XmlNamespaceManager(settings.NameTable);
            xmlns.AddNamespace("", "http://tempuri.org/datafile");
            var parser = new XmlParserContext(null, xmlns, String.Empty, XmlSpace.Default);

            // Load the data with our custom setings.
            using (var xr = XmlReader.Create(filePath, settings, parser))
            {
                ds.ReadXml(xr);
            }

            return ds;
        }
    }
}
