using System.Drawing;
using DrawingTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaintApp.Tests;

/// <summary>
/// MSTest suite for drawing tool strategies.
/// </summary>
[TestClass]
public class DrawingToolsTests
{
    [TestMethod]
    public void PencilTool_DisplayName_IsCreion()
    {
        Assert.AreEqual("Creion", new PencilTool().DisplayName);
    }

    [TestMethod]
    public void PencilTool_DrawsContinuously_IsTrue()
    {
        Assert.IsTrue(new PencilTool().DrawsContinuously);
    }

    [TestMethod]
    public void PencilTool_Draw_ChangesBitmap()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new PencilTool().Draw(graphics, new Point(6, 6), new Point(30, 30), pen);

        Assert.IsTrue(TestBitmapFactory.HasInk(bitmap));
    }

    [TestMethod]
    public void LineTool_DisplayName_IsLinie()
    {
        Assert.AreEqual("Linie", new LineTool().DisplayName);
    }

    [TestMethod]
    public void LineTool_Draw_ChangesBitmap()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new LineTool().Draw(graphics, new Point(5, 12), new Point(48, 12), pen);

        Assert.IsTrue(TestBitmapFactory.HasInk(bitmap));
    }

    [TestMethod]
    public void LineTool_Draw_MarksEndRegion()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new LineTool().Draw(graphics, new Point(8, 10), new Point(50, 40), pen);

        Assert.IsTrue(TestBitmapFactory.RegionHasInk(bitmap, new Point(50, 40), 3));
    }

    [TestMethod]
    public void RectangleTool_DisplayName_IsDreptunghi()
    {
        Assert.AreEqual("Dreptunghi", new RectangleTool().DisplayName);
    }

    [TestMethod]
    public void RectangleTool_Draw_ChangesBitmap()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new RectangleTool().Draw(graphics, new Point(10, 10), new Point(40, 35), pen);

        Assert.IsTrue(TestBitmapFactory.HasInk(bitmap));
    }

    [TestMethod]
    public void RectangleTool_Draw_NormalizesCoordinates()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new RectangleTool().Draw(graphics, new Point(42, 38), new Point(12, 16), pen);

        Assert.IsTrue(TestBitmapFactory.RegionHasInk(bitmap, new Point(12, 16), 3));
    }

    [TestMethod]
    public void RectangleTool_Draw_MarksBottomRightRegion()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new RectangleTool().Draw(graphics, new Point(10, 12), new Point(44, 34), pen);

        Assert.IsTrue(TestBitmapFactory.RegionHasInk(bitmap, new Point(44, 34), 3));
    }

    [TestMethod]
    public void EllipseTool_DisplayName_IsCerc()
    {
        Assert.AreEqual("Cerc", new EllipseTool().DisplayName);
    }

    [TestMethod]
    public void EllipseTool_Draw_ChangesBitmap()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new EllipseTool().Draw(graphics, new Point(10, 10), new Point(42, 42), pen);

        Assert.IsTrue(TestBitmapFactory.HasInk(bitmap));
    }

    [TestMethod]
    public void EllipseTool_Draw_NormalizesCoordinates()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new EllipseTool().Draw(graphics, new Point(44, 42), new Point(14, 12), pen);

        Assert.IsTrue(TestBitmapFactory.RegionHasInk(bitmap, new Point(29, 12), 3));
    }

    [TestMethod]
    public void EllipseTool_Draw_MarksTopArcRegion()
    {
        using var bitmap = TestBitmapFactory.CreateBlankBitmap();
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = TestBitmapFactory.CreatePen();

        new EllipseTool().Draw(graphics, new Point(12, 12), new Point(46, 46), pen);

        Assert.IsTrue(TestBitmapFactory.RegionHasInk(bitmap, new Point(29, 12), 3));
    }
}
