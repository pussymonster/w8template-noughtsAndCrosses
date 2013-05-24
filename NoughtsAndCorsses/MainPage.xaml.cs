using NoughtsAndCorsses.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NoughtsAndCorsses
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static BitmapImage emptyImage = new BitmapImage(new Uri ("ms-appx:///Assets/empty.png"));
        private static BitmapImage crossImage = new BitmapImage(new Uri("ms-appx:///Assets/cross.png"));
        private static BitmapImage nougthImage = new BitmapImage(new Uri("ms-appx:///Assets/nought.png"));
        private static int size = 25;


        private Model model;

        public MainPage()
        {
            this.InitializeComponent();
            initializeModel();
            initializeGrid();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void initializeGrid()
        {
            for (int i = 0; i < Math.Sqrt(size); i++)
            {
                ImagesGrid.RowDefinitions.Add(new RowDefinition());
                ImagesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for(int i = 0; i < Math.Sqrt(size); i++)
            {
                for(int j =0; j < Math.Sqrt(size); j++)
                {
                    Image image = new Image();
                    image.Tapped += this.Image_Tapped;
                    
                }
            }

             //<Image Name="Image1" Grid.Row="0" Grid.Column="0" Tag="0" Tapped="Image_Tapped"/>
             //       <Image Name="Image2" Grid.Row="0" Grid.Column="1" Tag="1" Tapped="Image_Tapped"/>
             //       <Image Name="Image3" Grid.Row="0" Grid.Column="2" Tag="2" Tapped="Image_Tapped"/>
             //       <Image Name="Image4" Grid.Row="1" Grid.Column="0" Tag="3" Tapped="Image_Tapped"/>
             //       <Image Name="Image5" Grid.Row="1" Grid.Column="1" Tag="4" Tapped="Image_Tapped"/>
             //       <Image Name="Image6" Grid.Row="1" Grid.Column="2" Tag="5" Tapped="Image_Tapped"/>
             //       <Image Name="Image7" Grid.Row="2" Grid.Column="0" Tag="6" Tapped="Image_Tapped"/>
             //       <Image Name="Image8" Grid.Row="2" Grid.Column="1" Tag="7" Tapped="Image_Tapped"/>
             //       <Image Name="Image9" Grid.Row="2" Grid.Column="2" Tag="8" Tapped="Image_Tapped"/>




            foreach (var item in ImagesGrid.Children)
            {
                if (item is Image)
                    (item as Image).Source = emptyImage;
                else if (item is Rectangle && (item as Rectangle).Tag != null && !(item as Rectangle).Tag.ToString().Equals("BoardRectangle"))
                    (item as Rectangle).Fill = ImagesGrid.Background;
            }
        }

        private void initializeModel()
        {
            model = new Model(size);
        }


        private void Reset_Clicked(object sender, RoutedEventArgs e)
        {
            initializeGrid();
            initializeModel();
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image image = sender as Image;
            int index = -1;
            if(int.TryParse(image.Tag.ToString(), out index)){
                if(model.isSet(index))
                {
                    model.setPlayer(index);
                    image.Source = model.CurrentPlayer.Equals(Model.Player.CROSS) ? crossImage : nougthImage;
                    EndCondition end = model.checkEnd();
                    if (end.End)
                    {
                        model.lockModel();
                        if (!end.Winner.Equals(Model.Player.EMPTY))
                        {
                            foreach (var item in ImagesGrid.Children)
                            {
                                if (item is Rectangle && (item as Rectangle).Tag != null)
                                {
                                    Rectangle rect = item as Rectangle;
                                    string rectTag = rect.Tag.ToString();
                                    if (rectTag.Equals(end.First.ToString()) || rectTag.Equals(end.Second.ToString()) || rectTag.Equals(end.Third.ToString()))
                                    {
                                        rect.Fill = new SolidColorBrush(Colors.White);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var message = new MessageDialog("It's a tie");
                            await message.ShowAsync();
                            initializeGrid();
                            initializeModel();
                        }


                    }
                    model.nextPlayer();
                }
            }
        }

    }
}
