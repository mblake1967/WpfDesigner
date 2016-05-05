using ICSharpCode.WpfDesign.Adorners;
using ICSharpCode.WpfDesign.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;

namespace MyTestAssembly
{
	[ExtensionFor(typeof(MyWidgetHolderView))]
	public class MyDisplaySizeExtension : PrimarySelectionAdornerProvider //SelectionAdornerProvider
	{
		readonly AdornerPanel adornerPanel;

		public MyDisplaySizeExtension()
		{
			adornerPanel = new AdornerPanel();
			adornerPanel.Order = AdornerOrder.Foreground; //Could be background for this POC, but just testing anyway.
			this.Adorners.Add(adornerPanel);

			//Create a textblock to hold some size info
			TextBlock tb = new TextBlock();
			tb.Text = "MyDisplaySizeExtension";

			AdornerPanel.SetPlacement(tb, new RelativePlacement(HorizontalAlignment.Right, VerticalAlignment.Bottom));
			adornerPanel.Children.Add(tb);
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			this.Services.Selection.PrimarySelectionChanged += OnPrimarySelectionChanged;

			OnPrimarySelectionChanged(null, null);
		}

		private void OnPrimarySelectionChanged(object sender, EventArgs e)
		{
			//May not need this for this extension poc

			//throw new NotImplementedException();
		}
	}
}
