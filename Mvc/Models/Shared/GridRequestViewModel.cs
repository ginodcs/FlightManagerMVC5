using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.Extensions.ModelBinders;
using ARQ.Maqueta.Presentation.Mvc.ViewModels;

namespace ARQ.Maqueta.Presentation.Mvc.Models.Shared
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    [ModelBinder(typeof(DataTablesGridRequestModelBinder))]
    public class GridRequestViewModel
    {
        /// <summary>
        /// Zero-based row start index
        /// </summary>
        public int RowStartIndex { get; set; }

        /// <summary>
        /// Number of rows to return
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Sort column name
        /// </summary>
        public string SortColumnName { get; set; }

        /// <summary>
        /// Sort direction (ascending | descending)
        /// </summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Current search string
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Custom data sent by the grid (optional)
        /// </summary>
        public string GridCustomData { get; set; }
    }
}