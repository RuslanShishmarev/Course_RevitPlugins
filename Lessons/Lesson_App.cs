using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using autodeskWnd = Autodesk.Windows;

namespace Lessons
{
    public class Lesson_App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyName = Assembly.GetExecutingAssembly().Location;

            string commandNamespace = "Lessons.Commands.";

            string tabName = "Курс RevitPlugins";
            application.CreateRibbonTab(tabName);

            RibbonPanel ribbonPanel1 = application.CreateRibbonPanel(tabName, "Начало");

            PushButtonData lesson1_BtnData = new PushButtonData(name: "Lesson_1", text: "Hello!", assemblyName: assemblyName, commandNamespace + "Lesson_1");
            PushButton lesson1_Btn = ribbonPanel1.AddItem(lesson1_BtnData) as PushButton;

            BitmapImage logo = new BitmapImage(new Uri("pack://application:,,,/Lessons;component/Images/logo.ico"));
            lesson1_Btn.LargeImage = logo;

            PushButtonData lesson2_BtnData = new PushButtonData(name: "Lesson_2", text: "Lesson_2", assemblyName: assemblyName, commandNamespace + "Lesson_2");
            lesson2_BtnData.LargeImage = logo;
            PushButtonData lesson3_BtnData = new PushButtonData(name: "Lesson_3", text: "Lesson_3", assemblyName: assemblyName, commandNamespace + "Lesson_3");
            lesson3_BtnData.LargeImage = logo;
            PushButtonData lesson4_BtnData = new PushButtonData(name: "Lesson_4", text: "Lesson_4", assemblyName: assemblyName, commandNamespace + "Lesson_4");
            lesson4_BtnData.LargeImage = logo;

            SplitButtonData splitButtonData_2_4 = new SplitButtonData("split", "lesson 2-4");
            SplitButton splitButton_2_4 = ribbonPanel1.AddItem(splitButtonData_2_4) as SplitButton;
            splitButton_2_4.AddPushButton(lesson2_BtnData);
            splitButton_2_4.AddPushButton(lesson3_BtnData);
            splitButton_2_4.AddPushButton(lesson4_BtnData);

            RibbonPanel ribbonPanel2 = application.CreateRibbonPanel(tabName, "Со скрытой частью");

            PushButtonData lesson5_BtnData = new PushButtonData(name: "Lesson_5", text: "Параметры", assemblyName: assemblyName, commandNamespace + "Lesson_5");
            lesson5_BtnData.Image = logo;

            PushButtonData lesson6_BtnData = new PushButtonData(name: "Lesson_6", text: "Колона", assemblyName: assemblyName, commandNamespace + "Lesson_6");
            lesson6_BtnData.Image = logo;

            PushButtonData lesson7_BtnData = new PushButtonData(name: "Lesson_7", text: "Стена", assemblyName: assemblyName, commandNamespace + "Lesson_7");
            lesson7_BtnData.Image = logo;

            ribbonPanel2.AddStackedItems(lesson5_BtnData, lesson6_BtnData, lesson7_BtnData);

            ribbonPanel2.AddSlideOut();

            PushButtonData lesson8_BtnData = new PushButtonData(name: "Lesson_8", text: "Окна", assemblyName: assemblyName, commandNamespace + "Lesson_8");
            PushButton lesson8_Btn = ribbonPanel2.AddItem(lesson8_BtnData) as PushButton;
            lesson8_Btn.LargeImage = logo;

            PushButtonData lesson9_BtnData = new PushButtonData(name: "Lesson_9", text: "На плоскости", assemblyName: assemblyName, commandNamespace + "Lesson_9");
            PushButton lesson9_Btn = ribbonPanel2.AddItem(lesson9_BtnData) as PushButton;
            lesson9_Btn.LargeImage = logo;

            RibbonPanel ribbonPanel3 = application.CreateRibbonPanel(tabName, "Окна плагина");

            PushButtonData modal = new PushButtonData(name: "Lesson_ModalWnd", text: "Модальное", assemblyName: assemblyName, commandNamespace + "Lesson_ModalWnd");
            modal.LargeImage = logo;

            PushButtonData modaless = new PushButtonData(name: "Lesson_ModalessWnd", text: "Немодальное", assemblyName: assemblyName, commandNamespace + "Lesson_ModalessWnd");
            modaless.LargeImage = logo;

            ComboBoxData comboBoxData = new ComboBoxData("ComboBox");

            ComboBoxMemberData first = new ComboBoxMemberData("first", "first");
            ComboBoxMemberData second = new ComboBoxMemberData("second", "second");

            var list =  ribbonPanel3.AddStackedItems(modal, modaless, comboBoxData);
            (list[2] as ComboBox).AddItems(new List<ComboBoxMemberData>() { first, second });

            //************************************************************************************

            BitmapImage logoImg = new BitmapImage(new Uri("pack://application:,,,/Lessons;component/Images/lg.ico"));

            ImageBrush logoBrush = new ImageBrush();
            logoBrush.ImageSource = logoImg;
            logoBrush.AlignmentY = AlignmentY.Center;
            logoBrush.AlignmentX = AlignmentX.Center;

            autodeskWnd.RibbonControl ribbon = autodeskWnd.ComponentManager.Ribbon;
            autodeskWnd.RibbonTab tab = ribbon.Tabs.FirstOrDefault(x => x.Name == tabName);
            autodeskWnd.RibbonPanel pnl = tab.Panels.FirstOrDefault(x => x.Source.Name == "Окна плагина");

            pnl.CustomPanelBackground = logoBrush;

            return Result.Succeeded;
        }
    }
}
