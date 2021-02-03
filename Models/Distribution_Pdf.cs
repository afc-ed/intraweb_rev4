using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;

namespace intraweb_rev3.Models
{
    public class Distribution_Pdf
    {
        public static void BatchPicklist(
            Distribution_Class.FormInput form, 
            string filePath, 
            List<Distribution_Class.BatchListStore> storeList,
            List<Distribution_Class.PicklistItem> pickList)
        {
            try
            {
                Document document = new Document();
                Section section1 = document.AddSection();
                section1.PageSetup.TopMargin = Unit.FromCentimeter(0.8);
                section1.PageSetup.BottomMargin = Unit.FromCentimeter(0.15);
                Table table1 = new Table();
                Column column = new Column();
                Row row1 = new Row();
                table1.Rows.LeftIndent = -53;
                table1.Borders.Width = 0.8;
                table1.Format.Font.Name = "Calibri";
                table1.Format.Font.Bold = false;
                table1.AddColumn(Unit.FromCentimeter(3.45)).Format.Alignment = ParagraphAlignment.Left;
                table1.AddColumn(Unit.FromCentimeter(2.45)).Format.Alignment = ParagraphAlignment.Left;
                for (int index = 1; index <= 10; ++index)
                {
                    table1.AddColumn(Unit.FromCentimeter(1.25)).Format.Alignment = ParagraphAlignment.Center;
                }
                table1.AddColumn(Unit.FromCentimeter(1.5)).Format.Alignment = ParagraphAlignment.Center;
                Row row2 = table1.AddRow();
                row2.Format.Font.Bold = true;
                row2.Cells[0].AddParagraph("Item");
                row2.Cells[0].Format.Font.Size = 12;
                row2.Cells[0].VerticalAlignment = VerticalAlignment.Top;
                row2.Cells[1].AddParagraph("Lot");
                row2.Cells[1].Format.Font.Size = 12;
                row2.Cells[1].VerticalAlignment = VerticalAlignment.Top;
                for (int index = 2; index <= 11; ++index)
                {
                    row2.Cells[index].AddParagraph((index - 1).ToString());
                    row2.Cells[index].Format.Font.Size = 16;
                }
                row2.Cells[12].AddParagraph("Total");
                row2.Cells[12].Format.Font.Size = 16;
                Row row3 = table1.AddRow();
                row3.VerticalAlignment = VerticalAlignment.Center;
                row3.Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                int num = 2;
                foreach (Distribution_Class.BatchListStore store in storeList)
                {
                    row3.Cells[num].AddParagraph(store.Code);
                    row3.Cells[num].Format.Font.Size = 7;
                    ++num;
                }
                bool flag = true;
                foreach (Distribution_Class.PicklistItem pick in pickList)
                {
                    Row row4 = table1.AddRow();
                    if (!flag)
                    {
                        row4.Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    row4.Cells[0].AddParagraph(pick.Name);
                    row4.Cells[0].VerticalAlignment = VerticalAlignment.Top;
                    row4.Cells[0].Format.Font.Size = 11;
                    row4.Cells[1].AddParagraph(pick.Lot);
                    row4.Cells[1].Format.Font.Size = 11;
                    row4.Cells[1].VerticalAlignment = VerticalAlignment.Top;
                    row4.Cells[2].AddParagraph(string.IsNullOrEmpty(pick.Qty1) ? string.Empty : pick.Qty1);
                    row4.Cells[2].Format.Font.Size = 15;
                    row4.Cells[3].AddParagraph(string.IsNullOrEmpty(pick.Qty2) ? string.Empty : pick.Qty2);
                    row4.Cells[3].Format.Font.Size = 15;
                    row4.Cells[4].AddParagraph(string.IsNullOrEmpty(pick.Qty3) ? string.Empty : pick.Qty3);
                    row4.Cells[4].Format.Font.Size = 15;
                    row4.Cells[5].AddParagraph(string.IsNullOrEmpty(pick.Qty4) ? string.Empty : pick.Qty4);
                    row4.Cells[5].Format.Font.Size = 15;
                    row4.Cells[6].AddParagraph(string.IsNullOrEmpty(pick.Qty5) ? string.Empty : pick.Qty5);
                    row4.Cells[6].Format.Font.Size = 15;
                    row4.Cells[7].AddParagraph(string.IsNullOrEmpty(pick.Qty6) ? string.Empty : pick.Qty6);
                    row4.Cells[7].Format.Font.Size = 15;
                    row4.Cells[8].AddParagraph(string.IsNullOrEmpty(pick.Qty7) ? string.Empty : pick.Qty7);
                    row4.Cells[8].Format.Font.Size = 15;
                    row4.Cells[9].AddParagraph(string.IsNullOrEmpty(pick.Qty8) ? string.Empty : pick.Qty8);
                    row4.Cells[9].Format.Font.Size = 15;
                    row4.Cells[10].AddParagraph(string.IsNullOrEmpty(pick.Qty9) ? string.Empty : pick.Qty9);
                    row4.Cells[10].Format.Font.Size = 15;
                    row4.Cells[11].AddParagraph(string.IsNullOrEmpty(pick.Qty10) ? string.Empty : pick.Qty10);
                    row4.Cells[11].Format.Font.Size = 15;
                    row4.Cells[12].AddParagraph(pick.LineTotal.ToString());
                    row4.Cells[12].Format.Font.Size = 15;
                }
                section1.Add(table1);
                // for store list, page 2.
                Section section2 = document.AddSection();
                section2.PageSetup.TopMargin = Unit.FromCentimeter(2.0);
                section2.AddParagraph(form.Batch).Format.Font.Size = 38;
                section2.AddParagraph(string.Empty).Format.LineSpacingRule = LineSpacingRule.Single;
                Table table2 = new Table();
                table2.Rows.LeftIndent = -53;
                table2.Borders = null;
                table2.Format.Font.Name = "Calibri";
                table2.Format.Font.Bold = false;
                table2.Format.Font.Size = 27;  //37
                table2.AddColumn(Unit.FromCentimeter(11)).Format.Alignment = ParagraphAlignment.Left;
                table2.AddColumn(Unit.FromCentimeter(2.25)).Format.Alignment = ParagraphAlignment.Left;
                table2.AddColumn(Unit.FromCentimeter(3.0)).Format.Alignment = ParagraphAlignment.Center;
                table2.AddColumn(Unit.FromCentimeter(3.7)).Format.Alignment = ParagraphAlignment.Center;
                num = 1;
                foreach (Distribution_Class.BatchListStore store in storeList)
                {
                    Row row = table2.AddRow();
                    string str1 = num++.ToString() + ". " + store.Name;
                    string str2 = str1.Length > 24 ? str1.Substring(0, 24) : str1;
                    row.Cells[0].AddParagraph(str2);
                    row.Cells[1].AddParagraph(store.State);
                    string str3 = store.ShipMethod.Length > 4 ? store.ShipMethod.Substring(0, 4) : store.ShipMethod;
                    row.Cells[2].AddParagraph(str3);
                    if (!string.IsNullOrEmpty(store.OrderNo))
                    {
                        row.Cells[3].AddParagraph("*" + store.OrderNo + "*");
                        row.Cells[3].Format.Font.Name = "Free 3 of 9 Extended";
                        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    }
                    // add line.
                    table2.AddRow();
                }
                section2.Add(table2);
                PdfDocumentRenderer documentRenderer = new PdfDocumentRenderer(false);
                documentRenderer.Document = document;
                documentRenderer.RenderDocument();
                documentRenderer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.BatchPicklist()");
            }
        }

        public static void PickTicket(Distribution_Class.FormInput form, string filePath)
        {
            try
            {
                Document document = new Document();
                Section section = document.AddSection();
                Table table1 = new Table();
                Column column1 = new Column();
                Row row1 = new Row();
                Row row2 = new Row();
                Row row3 = new Row();
                DataTable dataTable1 = Distribution_DB.BatchPicklist("orderpicklist_store", form.Batch);
                int num = 0;
                foreach (DataRow row in dataTable1.Rows)
                {
                    if (num > 0)
                    {
                        section = document.AddSection();
                    }
                    string orderNo = row["sopnumbe"].ToString();
                    section.PageSetup.TopMargin = Unit.FromCentimeter(6.7);
                    Table table2 = new Table();
                    table2.Rows.LeftIndent = -53;
                    table2.Borders.Width = 0.5;
                    table2.AddColumn(Unit.FromCentimeter(8.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(7.5)).Borders.Visible = false;
                    Row row5 = table2.AddRow();
                    row5.Cells[0].AddParagraph("AFC Distribution Corp.\n19205 South Laurel Park Rd.\nRancho Dominguez, CA. 90220\n\n");
                    row5.Cells[0].Format.Font.Name = "Arial";
                    row5.Cells[0].Format.Font.Size = 10;
                    row5.Cells[2].AddParagraph("*" + orderNo + "*").Format.Alignment = ParagraphAlignment.Center;
                    row5.Cells[2].Format.Font.Name = "Free 3 of 9 Extended";
                    row5.Cells[2].Format.Font.Size = 30;
                    section.Headers.Primary.Add(table2);
                    Table table3 = new Table();
                    table3.Rows.LeftIndent = -53;
                    table3.Borders.Width = 0.5;
                    table3.Format.Font.Name = "Calibri";
                    table3.Format.Font.Size = 11;
                    Column column2 = table3.AddColumn(Unit.FromCentimeter(8.0));
                    column2.Borders.Top.Visible = false;
                    column2.Borders.Bottom.Visible = false;
                    table3.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    table3.AddColumn(Unit.FromCentimeter(3.5)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    table3.AddColumn(Unit.FromCentimeter(4.0));
                    Row row6 = table3.AddRow();
                    row6.Cells[0].Borders.Top.Visible = true;
                    row6.Cells[0].AddParagraph("Ship To: " + row["custnmbr"].ToString());
                    row6.Cells[2].AddParagraph("Pick Ticket ");
                    row6.Cells[3].AddParagraph(orderNo);
                    Row row7 = table3.AddRow();
                    row7.Cells[0].AddParagraph(row["custname"].ToString());
                    row7.Cells[2].AddParagraph("Batch ");
                    row7.Cells[3].AddParagraph(form.Batch);
                    Row row8 = table3.AddRow();
                    row8.Cells[0].AddParagraph(row["address1"].ToString() + " " + row["address2"].ToString());
                    row8.Cells[2].AddParagraph("Doc. Date ");
                    row8.Cells[3].AddParagraph(row["docdate"].ToString());
                    Row row9 = table3.AddRow();
                    row9.Cells[0].AddParagraph(row["city"].ToString() + ", " + row["state"].ToString() + ".    " + row["zipcode"].ToString());
                    row9.Cells[2].AddParagraph("Ship Method ");
                    row9.Cells[3].AddParagraph(row["SHIPMTHD"].ToString());
                    Row row10 = table3.AddRow();
                    row10.Cells[0].Borders.Bottom.Visible = true;
                    row10.Cells[0].AddParagraph("Fran: " + row["fcid"].ToString() + "  |  Ph: " + row["fcphone"].ToString());
                    row10.Cells[2].AddParagraph("Ship Date ");
                    row10.Cells[3].AddParagraph(row["shipdate"].ToString().Trim());
                    section.Headers.Primary.Add(table3);
                    section.Headers.Primary.AddParagraph("\n\n");
                    Table table4 = new Table();
                    table4.Rows.LeftIndent = -53;
                    table4.Borders.Width = 0.5;
                    table4.Format.Font.Name = "Calibri";
                    table4.Format.Font.Size = 11;
                    column1 = table4.AddColumn(Unit.FromCentimeter(1.25));
                    column1 = table4.AddColumn(Unit.FromCentimeter(7.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(4.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(1.25));
                    column1 = table4.AddColumn(Unit.FromCentimeter(2.5));
                    column1 = table4.AddColumn(Unit.FromCentimeter(1.25));
                    column1 = table4.AddColumn(Unit.FromCentimeter(2.8));
                    Row row11 = table4.AddRow();
                    row11.Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    row11.Cells[0].AddParagraph("Item").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[1].AddParagraph("Description").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[2].AddParagraph("Lot").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[3].AddParagraph("Qty").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[4].AddParagraph("Location").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[5].AddParagraph("UOM").Format.Alignment = ParagraphAlignment.Center;
                    row11.Cells[6].AddParagraph("Qty Picked").Format.Alignment = ParagraphAlignment.Center;
                    section.Headers.Primary.Add(table4);
                    Table table5 = new Table();
                    table5.Rows.LeftIndent = -53;
                    table5.Borders.Width = 0.5;
                    table5.Format.Font.Name = "Calibri";
                    table5.Format.Font.Size = 11;
                    column1 = table5.AddColumn(Unit.FromCentimeter(1.25));
                    column1 = table5.AddColumn(Unit.FromCentimeter(7.0));
                    column1 = table5.AddColumn(Unit.FromCentimeter(4.0));
                    table5.AddColumn(Unit.FromCentimeter(1.25)).Format.Alignment = ParagraphAlignment.Right;
                    column1 = table5.AddColumn(Unit.FromCentimeter(2.5));
                    column1 = table5.AddColumn(Unit.FromCentimeter(1.25));
                    column1 = table5.AddColumn(Unit.FromCentimeter(2.8));
                    DataTable dataTable2 = Distribution_DB.BatchPicklist("orderpicklist_item_ver3", orderNo: orderNo);
                    List<Distribution_Pdf.Lot> itemLotsForOrder = Distribution_Pdf.GetItemLotsForOrder(orderNo);
                    foreach (DataRow rowItem in dataTable2.Rows)
                    {
                        Distribution_Class.Item obj = new Distribution_Class.Item();
                        obj.Number = rowItem["item"].ToString();
                        obj.Description = rowItem["itemdesc"].ToString();
                        obj.UOM = rowItem["uom"].ToString();
                        obj.UOMQty = Convert.ToInt32(rowItem["uomqty"]);
                        obj.Sold = Convert.ToInt32(rowItem["qty"]);
                        obj.LineSeq = Convert.ToInt32(rowItem["lineseq"]);
                        int int32 = Convert.ToInt32(rowItem["lotcount"]);
                        obj.Location = rowItem["location"].ToString();
                        Row row13 = table5.AddRow();
                        row13.Borders.Top.Visible = true;
                        row13.Borders.Bottom.Visible = false;
                        row13.Cells[0].Borders.Top.Visible = true;
                        row13.Cells[0].AddParagraph(obj.Number).Format.Alignment = ParagraphAlignment.Center;
                        row13.Cells[1].AddParagraph(obj.Description);
                        row13.Cells[2].AddParagraph();
                        row13.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                        row13.Cells[3].AddParagraph(obj.Sold.ToString());
                        row13.Cells[4].AddParagraph(obj.Location);
                        row13.Cells[5].AddParagraph(obj.UOM).Format.Alignment = ParagraphAlignment.Left;
                        row13.Cells[6].AddParagraph();
                        if (int32 > 0)
                        {
                            foreach (Distribution_Pdf.Lot lot in itemLotsForOrder)
                            {
                                if (obj.Number == lot.ItemNo && obj.LineSeq == lot.ItemLineSeq)
                                {
                                    Row row14 = table5.AddRow();
                                    row14.Borders.Top.Visible = false;
                                    row14.Borders.Bottom.Visible = false;
                                    row14.Cells[0].AddParagraph();
                                    row14.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                                    row14.Cells[2].AddParagraph(lot.Number).Format.Font.Bold = true;
                                    row14.Cells[2].Borders.Bottom.Clear();
                                    row14.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                                    row14.Cells[3].AddParagraph((lot.Qty / obj.UOMQty).ToString()).Format.Font.Bold = true;
                                    row14.Cells[6].AddParagraph("____________");
                                }
                            }
                        }
                        Row row15 = table5.AddRow();
                        row15.TopPadding = -4;
                        row15.Borders.Top.Visible = true;
                    }
                    section.Add(table5);
                    section.AddParagraph("\n\n");
                    Table table6 = new Table();
                    table6.Rows.LeftIndent = -53;
                    table6.Borders.Width = 0.5;
                    table6.Format.Font.Name = "Calibri";
                    table6.Format.Font.Size = 11;
                    Column column3 = table6.AddColumn(Unit.FromCentimeter(19.5));
                    column3.Borders.Top.Visible = false;
                    column3.Borders.Bottom.Visible = false;
                    Row row16 = table6.AddRow();
                    row16.Cells[0].Borders.Top.Visible = true;
                    row16.Cells[0].AddParagraph("Comment:");
                    Row row17 = table6.AddRow();
                    row17.Cells[0].Borders.Bottom.Visible = true;
                    row17.Cells[0].AddParagraph(row["CMMTTEXT"].ToString().Trim());
                    section.Add(table6);
                    section.Footers.Primary.AddParagraph("Page ").AddPageField();
                    ++num;
                }
                PdfDocumentRenderer documentRenderer = new PdfDocumentRenderer(false);
                documentRenderer.Document = document;
                documentRenderer.RenderDocument();
                documentRenderer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.OrderPicklist()");
            }
        }

        private static List<Distribution_Pdf.Lot> GetItemLotsForOrder(string orderNo)
        {
            try
            {
                Distribution_Pdf.Lot lot = new Distribution_Pdf.Lot();
                List<Distribution_Pdf.Lot> lotList = new List<Distribution_Pdf.Lot>();
                DataTable table = Distribution_DB.BatchPicklist("orderpicklist_item_lot", orderNo: orderNo);
                foreach (DataRow row in table.Rows)
                {
                    lot.ItemNo = row["item"].ToString();
                    lot.ItemLineSeq = Convert.ToInt32(row["lineseq"]);
                    lot.Number = row["lot"].ToString();
                    lot.Qty = Convert.ToInt32(row["qty"]);
                    lotList.Add(lot);
                    lot = new Distribution_Pdf.Lot();
                }
                return lotList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.GetItemLotsForOrder()");
            }
        }

        public static void ShippingTags(string filePath,
          Distribution_Class.FormInput form,
          string type,
          DataTable DropLabels)
        {
            try
            {
                PdfDocument pdfDocument = new PdfDocument();
                PdfPage pdfPage = pdfDocument.AddPage();
                pdfPage.Size = PageSize.Letter;
                XGraphics xgraphics = XGraphics.FromPdfPage(pdfPage);
                int num1 = 20;
                double num2 = 3.0;
                string str1 = "";
                DataTable table = Distribution_DB.BatchPicklist("shippingtag", form.Batch);
                foreach (DataRow row in table.Rows)
                {
                    if (str1 != "")
                    {
                        pdfPage = pdfDocument.AddPage();
                        xgraphics = XGraphics.FromPdfPage(pdfPage);
                    }
                    str1 = row["custname"].ToString().Trim();
                    string str2 = row["address1"].ToString().Trim();
                    string str3 = row["address2"].ToString().Trim() + " " + row["address3"].ToString().Trim();
                    string str4 = row["city"].ToString().Trim();
                    string str5 = row["state"].ToString().Trim();
                    string str6 = row["zipcode"].ToString().Trim();
                    bool flag = false;
                    foreach (DataRow row2 in (InternalDataCollectionBase)DropLabels.Rows)
                    {
                        if (str4.ToLower() == row2["city"].ToString().Trim().ToLower() && str5.ToLower() == row2["state"].ToString().Trim().ToLower())
                        {
                            flag = true;
                            break;
                        }
                    }
                    pdfPage.TrimMargins.All = 20;
                    double num3 = 10.0;
                    int num4 = 1;
                    XFont xfont1 = new XFont("Arial", num3 + 2.0, (XFontStyle)1);
                    XRect xrect;
                    xrect = new XRect((double)num1, num3, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Advanced Fresh Concepts Corp.", xfont1, (XBrush)XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num5 = num4 + 1;
                    XFont xfont2 = new XFont("Arial", num3, (XFontStyle)0);
                    xrect = new XRect((double)(num1 + 245), num3, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString(form.Batch, xfont2, (XBrush)XBrushes.Black, xrect, XStringFormats.TopRight);
                    xrect = new XRect((double)num1, (num2 + num3) * (double)num5, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("19205 South Laurel Park Road", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num6 = num5 + 1;
                    xrect = new XRect((double)num1, (num2 + num3) * (double)num6, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Rancho Dominguez,  CA.  90220", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num7 = num6 + 1;
                    xrect = new XRect((double)num1, (num2 + num3) * (double)num7, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Phone: 310-604-3200", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    if (type == "frozen")
                    {
                        double num8 = 36.0;
                        XFont xfont3 = new XFont("Arial", num8, (XFontStyle)3);
                        xrect = new XRect(pdfPage.Width / 2.0 + 170.0, num8, 100.0, num8);
                        xgraphics.DrawString("Frozen", xfont3, XBrushes.Black, xrect, XStringFormats.Center);
                        xrect = new XRect(pdfPage.Width / 2.0 + 155.0, num8, 130.0, num8);
                        XPen xpen = new XPen(XColors.Black, 2.0);
                        xgraphics.DrawRectangle(xpen, xrect);
                    }
                    if (flag)
                    {
                        double num8 = 36.0;
                        XFont xfont3 = new XFont("Arial", num8, (XFontStyle)1);
                        xrect = new XRect(pdfPage.Width / 2.0 + 170.0, num8 + 150.0, 100.0, num8);
                        xgraphics.DrawString("DROP", xfont3, XBrushes.Black, xrect, XStringFormats.Center);
                    }
                    double num9 = 36.0;
                    int num10 = 1;
                    double num11 = pdfPage.Height / 2.0 - 310.0;
                    XFont xfont4 = new XFont("Arial", num9, (XFontStyle)5);
                    xrect = new XRect((double)num1, num11, pdfPage.Width, num9);
                    xgraphics.DrawString(str1, xfont4, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num12 = num10 + 1;
                    double num13 = 27.0;
                    XFont xfont5 = new XFont("Arial", num13, (XFontStyle)0);
                    xrect = new XRect((double)num1, num11 + (num2 + num13) * (double)num12, pdfPage.Width, num13);
                    xgraphics.DrawString(str2, xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num14 = num12 + 1;
                    if (str3 != " ")
                    {
                        xrect = new XRect((double)num1, num11 + (num2 + num13) * (double)num14, pdfPage.Width, num13);
                        xgraphics.DrawString(str3, xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                        ++num14;
                    }
                    xrect = new XRect((double)num1, num11 + (num2 + num13) * (double)num14, pdfPage.Width, num13);
                    xgraphics.DrawString(str4 + ",  " + str5 + ".  " + str6, xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num15 = num14 + 1;
                    xrect = new XRect((double)num1, num11 + (num2 + num13) * (double)num15, pdfPage.Width, num13);
                    xgraphics.DrawString("Attn: Sushi Bar", xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    double num16 = 10.0;
                    int num17 = 1;
                    double num18 = pdfPage.Height / 2.0 + 10.0;
                    XFont xfont6 = new XFont("Arial", num16 + 2.0, (XFontStyle)1);
                    xrect = new XRect((double)num1, num18 + num16, pdfPage.Width / 2.0, num16);
                    xgraphics.DrawString("Advanced Fresh Concepts Corp.", xfont6, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num19 = num17 + 1;
                    XFont xfont7 = new XFont("Arial", num16, (XFontStyle)0);
                    xrect = new XRect((double)num1, num18 + (num2 + num16) * (double)num19, pdfPage.Width / 2.0, num16);
                    xgraphics.DrawString("19205 South Laurel Park Road", xfont7, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num20 = num19 + 1;
                    xrect = new XRect((double)num1, num18 + (num2 + num16) * (double)num20, pdfPage.Width / 2.0, num16);
                    xgraphics.DrawString("Rancho Dominguez, CA. 90220", xfont7, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num21 = num20 + 1;
                    xrect = new XRect((double)num1, num18 + (num2 + num16) * (double)num21, pdfPage.Width / 2.0, num16);
                    xgraphics.DrawString("Ph: 310-604-3200", xfont7, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    if (type == "frozen")
                    {
                        double num8 = 36.0;
                        XFont xfont3 = new XFont("Arial", num8, (XFontStyle)3);
                        xrect = new XRect(pdfPage.Width / 2.0 + 170.0, num18 + num8, 100.0, num8);
                        xgraphics.DrawString("Frozen", xfont3, XBrushes.Black, xrect, XStringFormats.Center);
                        xrect = new XRect(pdfPage.Width / 2.0 + 155.0, num18 + num8, 130.0, num8);
                        XPen xpen = new XPen(XColors.Black, 2.0);
                        xgraphics.DrawRectangle(xpen, xrect);
                    }
                    if (flag)
                    {
                        double num8 = 36.0;
                        XFont xfont3 = new XFont("Arial", num8, (XFontStyle)1);
                        xrect = new XRect(pdfPage.Width / 2.0 + 170.0, num18 + num8 + 150.0, 100.0, num8);
                        xgraphics.DrawString("DROP", xfont3, XBrushes.Black, xrect, XStringFormats.Center);
                    }
                    double num22 = 36.0;
                    int num23 = 1;
                    double num24 = pdfPage.Height / 2.0 + 100.0;
                    XFont xfont8 = new XFont("Arial", num22, (XFontStyle)5);
                    xrect = new XRect((double)num1, num24, pdfPage.Width, num22);
                    xgraphics.DrawString(str1, xfont8, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num25 = num23 + 1;
                    double num26 = 27.0;
                    XFont xfont9 = new XFont("Arial", num26, (XFontStyle)0);
                    xrect = new XRect((double)num1, num24 + (num2 + num26) * (double)num25, pdfPage.Width, num26);
                    xgraphics.DrawString(str2, xfont9, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num27 = num25 + 1;
                    if (str3 != " ")
                    {
                        xrect = new XRect((double)num1, num24 + (num2 + num26) * (double)num27, pdfPage.Width, num26);
                        xgraphics.DrawString(str3, xfont9, XBrushes.Black, xrect, XStringFormats.TopLeft);
                        ++num27;
                    }
                    xrect = new XRect((double)num1, num24 + (num2 + num26) * (double)num27, pdfPage.Width, num26);
                    xgraphics.DrawString(str4 + ",  " + str5 + ".  " + str6, xfont9, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num28 = num27 + 1;
                    xrect = new XRect((double)num1, num24 + (num2 + num26) * (double)num28, pdfPage.Width, num26);
                    xgraphics.DrawString("Attn: Sushi Bar", xfont9, XBrushes.Black, xrect, XStringFormats.TopLeft);
                }
                pdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.ShippingTags()");
            }
        }

        public static void PalletCount(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                new PdfPage().Size = PageSize.Letter;
                PdfDocument pdfDocument = new PdfDocument();
                PdfPage pdfPage = pdfDocument.AddPage();
                pdfPage.Orientation = PageOrientation.Landscape;
                XGraphics xgraphics = XGraphics.FromPdfPage(pdfPage);
                double num1 = 20.0;
                double num2 = 3.0;
                string str1 = "";
                DataTable table = Distribution_DB.BatchPicklist("shippingtag", form.Batch);
                foreach (DataRow row in table.Rows)
                {
                    if (str1 != "")
                    {
                        pdfPage = pdfDocument.AddPage();
                        pdfPage.Orientation = PageOrientation.Landscape;
                        xgraphics = XGraphics.FromPdfPage(pdfPage);
                    }
                    str1 = row["custname"].ToString().Trim();
                    string str2 = row["state"].ToString().Trim();
                    string str3 = row["shipmthd"].ToString().Trim();
                    double num3 = 10.0;
                    int num4 = 1;
                    double num5 = num3 + 10.0;
                    XFont xfont1 = new XFont("Arial", num3 + 2.0, XFontStyle.Bold);
                    XRect xrect;
                    xrect = new XRect(num1, num5, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Advanced Fresh Concepts Corp.", xfont1, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num6 = num4 + 1;
                    XFont xfont2 = new XFont("Arial", num3, XFontStyle.Bold);
                    xrect = new XRect(num1 + 345.0, num5, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString(form.Batch, xfont2, XBrushes.Black, xrect, XStringFormats.TopRight);
                    xrect = new XRect(num1, (num2 + num5 - 5.0) * (double)num6, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("19205 South Laurel Park Road", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num7 = num6 + 1;
                    xrect = new XRect(num1, (num2 + num5 - 7.0) * (double)num7, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Rancho Dominguez,  CA.  90220", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    int num8 = num7 + 1;
                    xrect = new XRect(num1, (num2 + num5 - 8.0) * (double)num8, pdfPage.Width / 2.0, num3);
                    xgraphics.DrawString("Phone: 310-604-3200", xfont2, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    double num9 = 35.0;
                    double num10 = 150.0;
                    XFont xfont3 = new XFont("Arial", num9, XFontStyle.Bold);
                    XFont xfont4 = new XFont("Arial", 75.0, XFontStyle.Bold);
                    xrect = new XRect(num1, num10 - 30.0, pdfPage.Width, num9);
                    xgraphics.DrawString(str1 + " (" + str2 + ")", xfont4, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    XFont xfont5 = new XFont("Arial", num9, XFontStyle.Bold);
                    double num11 = num10 + 100.0;
                    xrect = new XRect(num1, num11, pdfPage.Width, num9);
                    xgraphics.DrawString("Carrier:  " + str3, xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    double num12 = num11 + 100.0;
                    xrect = new XRect(num1, num12, 50.0, num9);
                    xgraphics.DrawString("Pallets: ", xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    xgraphics.DrawLine(XPens.Black, num1 + 150.0, num12 + num9, num1 + 370.0, num12 + num9);
                    xrect = new XRect(num1 + 400.0, num12, 200.0, num9);
                    xgraphics.DrawString("Of: ", xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    xgraphics.DrawLine(XPens.Black, num1 + 480.0, num12 + num9, num1 + 700.0, num12 + num9);
                    double num13 = num12 + 100.0;
                    xrect = new XRect(num1, num13, 50.0, num9);
                    xgraphics.DrawString("# Of Cases: ", xfont5, XBrushes.Black, xrect, XStringFormats.TopLeft);
                    xgraphics.DrawLine(XPens.Black, num1 + 200.0, num13 + num9, num1 + 700.0, num13 + num9);
                }
                pdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.PalletCount()");
            }
        }

        public static void IntransitBillOfLading(Distribution_Class.BillofLading lading, string filePath)
        {
            try
            {
                Document document = new Document();
                Section section = document.AddSection();
                Table table1 = new Table();
                Column column1 = new Column();
                Row row1 = new Row();
                DataTable dataTable = Distribution_DB.InTransitBillOfLading("document", lading);
                int num = 0;
                foreach (DataRow row2 in (InternalDataCollectionBase)dataTable.Rows)
                {
                    lading.Customer = row2["name"].ToString();
                    lading.Street1 = row2["addr1"].ToString();
                    lading.Street2 = row2["addr2"].ToString();
                    lading.City = row2["city"].ToString();
                    lading.State = row2["st"].ToString();
                    lading.Zip = row2["zip"].ToString();
                    lading.Phone = Utilities.FormatPhone(row2["phone1"].ToString());
                    lading.FrozenWeight = Math.Truncate(Convert.ToDecimal(row2["frozen"]));
                    lading.DryWeight = Math.Truncate(Convert.ToDecimal(row2["dry"]));
                    lading.TotalWeight = lading.FrozenWeight + lading.DryWeight;
                    if (num > 0)
                        section = document.AddSection();
                    section.PageSetup.TopMargin = Unit.FromCentimeter(6.7);
                    Table table2 = new Table();
                    table2.Rows.LeftIndent = -53;
                    table2.Borders.Width = 0.5;
                    table2.Format.Font.Name = "Calibri";
                    table2.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(11.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    Row row3 = table2.AddRow();
                    row3.Cells[1].AddParagraph("BILL OF LADING").Format.Alignment = ParagraphAlignment.Center;
                    row3.Cells[1].Format.Font.Bold = true;
                    row3.Cells[1].Format.Font.Size = 24;
                    row3.Cells[2].AddParagraph("#" + lading.DocNumber).Format.Alignment = ParagraphAlignment.Right;
                    row3.Cells[2].Format.Font.Size = 18;
                    row3.Cells[2].Format.Font.Bold = true;
                    section.Headers.Primary.Add(table2);
                    section.Headers.Primary.AddParagraph("\n\n");
                    Table table3 = new Table();
                    table3.Rows.LeftIndent = -53;
                    table3.Borders.Width = 0.5;
                    table3.Format.Font.Name = "Calibri";
                    table3.Format.Font.Size = 12;
                    column1 = table3.AddColumn(Unit.FromCentimeter(8.0));
                    table3.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    column1 = table3.AddColumn(Unit.FromCentimeter(8.0));
                    Row row4 = table3.AddRow();
                    row4.Cells[0].AddParagraph("SHIP TO");
                    row4.Cells[0].Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    row4.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                    row4.Cells[2].AddParagraph("SHIP FROM");
                    row4.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                    row4.Cells[2].Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    Row row5 = table3.AddRow();
                    row5.Cells[0].AddParagraph(lading.Customer + "\n" + lading.Street1 + " " + lading.Street2 + "\n" + lading.City + ",  " + lading.State + ".   " + lading.Zip + "\n" + lading.Phone.Remove(13));
                    row5.Cells[2].AddParagraph("AFC CORP\n19205 Laurel Park Road\nRancho Dominguez, CA.  90220\n310-604-8257");
                    section.Headers.Primary.Add(table3);
                    section.Headers.Primary.AddParagraph("\n");
                    Table table4 = new Table();
                    table4.Rows.LeftIndent = -53;
                    table4.Borders.Width = 0.5;
                    table4.Format.Font.Name = "Calibri";
                    table4.Format.Font.Size = 12;
                    column1 = table4.AddColumn(Unit.FromCentimeter(3.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(9.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(4.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(4.0));
                    Row row6 = table4.AddRow();
                    row6.Cells[0].AddParagraph("Quantity").Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[0].Format.Font.Bold = true;
                    row6.Cells[1].AddParagraph("Description").Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[1].Format.Font.Bold = true;
                    row6.Cells[2].AddParagraph("Weight\n(Subject to Correction)").Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[2].Format.Font.Bold = true;
                    row6.Cells[3].AddParagraph("Class").Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[3].Format.Font.Bold = true;
                    section.Add(table4);
                    Table table5 = new Table();
                    table5.Rows.LeftIndent = -53;
                    table5.Borders.Width = 0.5;
                    table5.Format.Font.Name = "Calibri";
                    table5.Format.Font.Size = 14;
                    table5.Rows.Height = Unit.FromCentimeter(0.9);
                    column1 = table5.AddColumn(Unit.FromCentimeter(3.0));
                    table5.AddColumn(Unit.FromCentimeter(9.0)).RightPadding = Unit.FromCentimeter(1.5);
                    table5.AddColumn(Unit.FromCentimeter(4.0)).Format.Alignment = ParagraphAlignment.Right;
                    table5.AddColumn(Unit.FromCentimeter(4.0)).Format.Alignment = ParagraphAlignment.Right;
                    Row row7 = table5.AddRow();
                    row7.Cells[1].AddParagraph("1   Pallets Frozen").Format.Alignment = ParagraphAlignment.Right;
                    row7.Cells[1].Format.Font.Bold = true;
                    row7.Cells[2].AddParagraph(lading.FrozenWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row7.Cells[2].Format.Font.Bold = true;
                    Row row8 = table5.AddRow();
                    row8.Cells[1].AddParagraph("Pallets Chilled").Format.Alignment = ParagraphAlignment.Right;
                    row8.Cells[1].Format.Font.Bold = true;
                    row8.Cells[2].AddParagraph(lading.DryWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row8.Cells[2].Format.Font.Bold = true;
                    Row row9 = table5.AddRow();
                    row9.Cells[1].AddParagraph("Total").Format.Alignment = ParagraphAlignment.Right;
                    row9.Cells[1].Format.Font.Bold = true;
                    row9.Cells[2].AddParagraph(lading.TotalWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row9.Cells[2].Format.Font.Bold = true;
                    table5.AddRow();
                    table5.AddRow();
                    table5.AddRow();
                    Row row10 = table5.AddRow();
                    row10.Cells[1].AddParagraph("Please Call Before Delivery").Format.Alignment = ParagraphAlignment.Left;
                    row10.Cells[1].Format.Font.Bold = true;
                    table5.AddRow();
                    section.Add(table5);
                    section.AddParagraph("\n\tAll cartons are marked ____________\n\n");
                    Table table6 = new Table();
                    table6.Rows.LeftIndent = -53;
                    table6.Borders.Width = 0.5;
                    table6.Format.Font.Name = "Calibri";
                    table6.Format.Font.Size = 11;
                    table6.Borders.Visible = true;
                    table6.Rows.Height = Unit.FromCentimeter(0.6);
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(5.0));
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(3.0));
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(3.0));
                    Row row11 = table6.AddRow();
                    row11.Cells[0].AddParagraph("Remit COD to:").Format.Alignment = ParagraphAlignment.Right;
                    row11.Cells[2].AddParagraph("COD Fee:").Format.Alignment = ParagraphAlignment.Right;
                    row11.Cells[4].AddParagraph("Freight Charges:").Format.Alignment = ParagraphAlignment.Right;
                    Row row12 = table6.AddRow();
                    row12.Cells[0].AddParagraph("Address:").Format.Alignment = ParagraphAlignment.Right;
                    row12.Cells[0].Borders.Bottom.Visible = false;
                    row12.Cells[2].AddParagraph("Prepaid:").Format.Alignment = ParagraphAlignment.Right;
                    row12.Cells[4].AddParagraph("Prepaid:").Format.Alignment = ParagraphAlignment.Right;
                    Row row13 = table6.AddRow();
                    row13.Cells[2].AddParagraph("Collect $:").Format.Alignment = ParagraphAlignment.Right;
                    row13.Cells[4].AddParagraph("Collect $:").Format.Alignment = ParagraphAlignment.Right;
                    section.Add(table6);
                    section.AddParagraph("\n");
                    Table table7 = new Table();
                    table7.Rows.LeftIndent = -53;
                    table7.Borders.Width = 0.5;
                    table7.Format.Font.Name = "Calibri";
                    table7.Format.Font.Size = 11;
                    table7.Borders.Visible = false;
                    column1 = table7.AddColumn(Unit.FromCentimeter(19.5));
                    table7.AddRow().Cells[0].AddParagraph("Received subject to the classifications and tariffs in effect on the date of the issue of the Bill of Lading, the \nproperty described above in apparent good order, except as noted (contents and condition of contents of \npackages unknown), marked, consigned, and destined as indicated above which said carrier (the word carrier \nbeing understood throughout this contract as meaning any person or corporation in possession of the property \nunder the contract) agrees to carry to its usual place of delivery at said destination, if on its route, otherwise to \ndelivery to another carrier on the route to said destination.  It is mutually agreed as to each carrier of all or any \nof, said property over all or any portion of said route to destination and as to each party at any time interested in \nall or any said property, that every service to be performed hereunder shall be to all the bill of lading terms and \nconditions in the governing classification and the said terms and conditions are hereby agreed to by the shipper \nand accepted for himself and his assigns.");
                    section.Add(table7);
                    section.AddParagraph("\n\n");
                    Table table8 = new Table();
                    table8.Rows.LeftIndent = -53;
                    table8.Borders.Width = 0.5;
                    table8.Format.Font.Name = "Calibri";
                    table8.Format.Font.Size = 11;
                    table8.Rows.Height = Unit.FromCentimeter(0.9);
                    column1 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    column1 = table8.AddColumn(Unit.FromCentimeter(6.0));
                    column1 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    Column column2 = table8.AddColumn(Unit.FromCentimeter(4.0));
                    column2.Borders.Left.Visible = false;
                    column2.Borders.Right.Visible = false;
                    Column column3 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    column3.Borders.Left.Visible = false;
                    column3.Borders.Right.Visible = false;
                    column1 = table8.AddColumn(Unit.FromCentimeter(3.0));
                    Row row14 = table8.AddRow();
                    row14.Cells[0].AddParagraph("Shipper:").Format.Alignment = ParagraphAlignment.Right;
                    row14.Cells[0].Format.Borders.Bottom.Visible = false;
                    row14.Cells[1].AddParagraph("AFC Corp.").Format.Font.Bold = true;
                    row14.Cells[0].Format.Borders.Bottom.Visible = false;
                    row14.Cells[2].AddParagraph("Carrier:").Format.Alignment = ParagraphAlignment.Right;
                    row14.Cells[2].Format.Borders.Bottom.Visible = false;
                    row14.Cells[3].Format.Borders.Bottom.Visible = false;
                    Row row15 = table8.AddRow();
                    row15.Cells[0].AddParagraph("Per:").Format.Alignment = ParagraphAlignment.Right;
                    row15.Cells[2].AddParagraph("Per:").Format.Alignment = ParagraphAlignment.Right;
                    row15.Cells[4].AddParagraph("Date:").Format.Alignment = ParagraphAlignment.Right;
                    section.Add(table8);
                    section.Footers.Primary.AddParagraph("Page ").AddPageField();
                    ++num;
                }
                PdfDocumentRenderer documentRenderer = new PdfDocumentRenderer(false);
                documentRenderer.Document = document;
                documentRenderer.RenderDocument();
                documentRenderer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.IntransitBillOfLading()");
            }
        }

        public static void BillofLading(string filePath, Distribution_Class.FormInput form)
        {
            try
            {
                Distribution_Class.BillofLading billofLading = new Distribution_Class.BillofLading();
                Document document = new Document();
                Section section = document.AddSection();
                Table table1 = new Table();
                Column column1 = new Column();
                //Row row1 = new Row();
                DataTable dt = Distribution_DB.SalesBillofLading(form.Batch);
                int num = 0;
                foreach (DataRow row in dt.Rows)
                {
                    billofLading.DocNumber = row["orderno"].ToString();
                    billofLading.Customer = row["custno"].ToString();
                    billofLading.CustomerName = row["custname"].ToString();
                    billofLading.Street1 = row["addr1"].ToString();
                    billofLading.Street2 = row["addr2"].ToString();
                    billofLading.City = row["city"].ToString();
                    billofLading.State = row["st"].ToString();
                    billofLading.Zip = row["zip"].ToString();
                    billofLading.Phone = Utilities.FormatPhone(row["custphone"].ToString());
                    billofLading.FrozenWeight = Math.Truncate(Convert.ToDecimal(row["frozen"]));
                    billofLading.DryWeight = Math.Truncate(Convert.ToDecimal(row["dry"]));
                    billofLading.TotalWeight = billofLading.FrozenWeight + billofLading.DryWeight;
                    billofLading.FCID = row["fcid"].ToString();
                    billofLading.FCPhone = Utilities.FormatPhone(row["fcphone"].ToString());
                    billofLading.Shipper = row["carrier"].ToString().ToUpper();
                    billofLading.ShipperPhone = Utilities.FormatPhone(row["carrierphone"].ToString());
                    // add section for each new record.
                    if (num > 0)
                    {
                        section = document.AddSection();
                    }
                    section.PageSetup.TopMargin = Unit.FromCentimeter(6.7);
                    Table table2 = new Table();
                    table2.Rows.LeftIndent = -53;
                    table2.Borders.Width = 0.5;
                    table2.Format.Font.Name = "Calibri";
                    table2.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(11.0)).Borders.Visible = false;
                    table2.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    Row row3 = table2.AddRow();
                    row3.Cells[1].AddParagraph(billofLading.Shipper).Format.Alignment = ParagraphAlignment.Center;
                    row3.Cells[1].Format.Font.Bold = true;
                    row3.Cells[1].Format.Font.Size = 16;
                    Row row4 = table2.AddRow();
                    row4.Cells[1].AddParagraph(billofLading.ShipperPhone).Format.Alignment = ParagraphAlignment.Center;
                    row4.Cells[1].Format.Font.Size = 12;
                    row4.Cells[2].AddParagraph("#" + billofLading.DocNumber.Replace("A", "")).Format.Alignment = ParagraphAlignment.Right;
                    row4.Cells[2].Format.Font.Size = 14;
                    row4.Cells[2].Format.Font.Bold = true;
                    Row row5 = table2.AddRow();
                    row5.Cells[1].AddParagraph("BILL OF LADING").Format.Alignment = ParagraphAlignment.Center;
                    row5.Cells[1].Format.Font.Bold = true;
                    row5.Cells[1].Format.Font.Size = 24;
                    row5.Cells[2].AddParagraph("Order# " + billofLading.DocNumber).Format.Alignment = ParagraphAlignment.Right;
                    row5.Cells[2].Format.Font.Size = 11;
                    section.Headers.Primary.Add(table2);
                    section.Headers.Primary.AddParagraph("\n");
                    Table table3 = new Table();
                    table3.Rows.LeftIndent = -53;
                    table3.Borders.Width = 0.5;
                    table3.Format.Font.Name = "Calibri";
                    table3.Format.Font.Size = 12;
                    column1 = table3.AddColumn(Unit.FromCentimeter(8.0));
                    table3.AddColumn(Unit.FromCentimeter(4.0)).Borders.Visible = false;
                    column1 = table3.AddColumn(Unit.FromCentimeter(8.0));
                    Row row6 = table3.AddRow();
                    row6.Cells[0].AddParagraph("SHIP TO");
                    row6.Cells[0].Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    row6.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[2].AddParagraph("SHIP FROM");
                    row6.Cells[2].Format.Alignment = ParagraphAlignment.Center;
                    row6.Cells[2].Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    Row row7 = table3.AddRow();
                    row7.Cells[0].AddParagraph(billofLading.Customer + "\n" + billofLading.CustomerName + "\n" + billofLading.Street1 + " " + billofLading.Street2 + "\n" + billofLading.City + ",  " + billofLading.State + ".   " + billofLading.Zip + "\nStore Ph: " + billofLading.Phone + "\nAttn. Sushi Bar, Cell: " + billofLading.FCPhone);
                    row7.Cells[2].AddParagraph("AFC CORP\n19205 Laurel Park Road\nRancho Dominguez, CA.  90220\n310-604-8257");
                    section.Headers.Primary.Add(table3);
                    section.Headers.Primary.AddParagraph("\n");
                    section.PageSetup.TopMargin = Unit.FromCentimeter(8.0);
                    Table table4 = new Table();
                    table4.Rows.LeftIndent = -53;
                    table4.Borders.Width = 0.5;
                    table4.Format.Font.Name = "Calibri";
                    table4.Format.Font.Size = 10;
                    column1 = table4.AddColumn(Unit.FromCentimeter(3.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(9.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(4.0));
                    column1 = table4.AddColumn(Unit.FromCentimeter(4.0));
                    Row row8 = table4.AddRow();
                    row8.Cells[0].AddParagraph("Quantity").Format.Alignment = ParagraphAlignment.Center;
                    row8.Cells[0].Format.Font.Bold = true;
                    row8.Cells[1].AddParagraph("Description").Format.Alignment = ParagraphAlignment.Center;
                    row8.Cells[1].Format.Font.Bold = true;
                    row8.Cells[2].AddParagraph("Weight\n(Subject to Correction)").Format.Alignment = ParagraphAlignment.Center;
                    row8.Cells[2].Format.Font.Bold = true;
                    row8.Cells[3].AddParagraph("Class").Format.Alignment = ParagraphAlignment.Center;
                    row8.Cells[3].Format.Font.Bold = true;
                    section.Add(table4);
                    Table table5 = new Table();
                    table5.Rows.LeftIndent = -53;
                    table5.Borders.Width = 0.5;
                    table5.Format.Font.Name = "Calibri";
                    table5.Format.Font.Size = 14;
                    table5.Rows.Height = Unit.FromCentimeter(0.8);
                    column1 = table5.AddColumn(Unit.FromCentimeter(3.0));
                    table5.AddColumn(Unit.FromCentimeter(9.0)).RightPadding = Unit.FromCentimeter(1.5);
                    table5.AddColumn(Unit.FromCentimeter(4.0)).Format.Alignment = ParagraphAlignment.Right;
                    table5.AddColumn(Unit.FromCentimeter(4.0)).Format.Alignment = ParagraphAlignment.Right;
                    Row row9 = table5.AddRow();
                    row9.Cells[1].AddParagraph("1   Pallets Frozen").Format.Alignment = ParagraphAlignment.Right;
                    row9.Cells[1].Format.Font.Bold = true;
                    row9.Cells[2].AddParagraph(billofLading.FrozenWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row9.Cells[2].Format.Font.Bold = true;
                    Row row10 = table5.AddRow();
                    row10.Cells[1].AddParagraph("Pallets Chilled").Format.Alignment = ParagraphAlignment.Right;
                    row10.Cells[1].Format.Font.Bold = true;
                    row10.Cells[2].AddParagraph(billofLading.DryWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row10.Cells[2].Format.Font.Bold = true;
                    Row row11 = table5.AddRow();
                    row11.Cells[1].AddParagraph("Total").Format.Alignment = ParagraphAlignment.Right;
                    row11.Cells[1].Format.Font.Bold = true;
                    row11.Cells[2].AddParagraph(billofLading.TotalWeight.ToString()).Format.Alignment = ParagraphAlignment.Right;
                    row11.Cells[2].Format.Font.Bold = true;
                    table5.AddRow();
                    table5.AddRow();
                    table5.AddRow();
                    Row row12 = table5.AddRow();
                    row12.Cells[1].AddParagraph("Please Call Before Delivery").Format.Alignment = ParagraphAlignment.Left;
                    row12.Cells[1].Format.Font.Bold = true;
                    table5.AddRow();
                    section.Add(table5);
                    section.AddParagraph("\n\tAll cartons are marked ____________\n\n");
                    Table table6 = new Table();
                    table6.Rows.LeftIndent = -53;
                    table6.Borders.Width = 0.5;
                    table6.Format.Font.Name = "Calibri";
                    table6.Format.Font.Size = 11;
                    table6.Borders.Visible = true;
                    table6.Rows.Height = Unit.FromCentimeter(0.6);
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(5.0));
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(3.0));
                    table6.AddColumn(Unit.FromCentimeter(3.0)).Shading.Color = Color.FromCmyk(0.0, 0.0, 0.0, 17.0);
                    column1 = table6.AddColumn(Unit.FromCentimeter(3.0));
                    Row row13 = table6.AddRow();
                    row13.Cells[0].AddParagraph("Remit COD to:").Format.Alignment = ParagraphAlignment.Right;
                    row13.Cells[2].AddParagraph("COD Fee:").Format.Alignment = ParagraphAlignment.Right;
                    row13.Cells[4].AddParagraph("Freight Charges:").Format.Alignment = ParagraphAlignment.Right;
                    Row row14 = table6.AddRow();
                    row14.Cells[0].AddParagraph("Address:").Format.Alignment = ParagraphAlignment.Right;
                    row14.Cells[0].Borders.Bottom.Visible = false;
                    row14.Cells[2].AddParagraph("Prepaid:").Format.Alignment = ParagraphAlignment.Right;
                    row14.Cells[4].AddParagraph("Prepaid:").Format.Alignment = ParagraphAlignment.Right;
                    Row row15 = table6.AddRow();
                    row15.Cells[2].AddParagraph("Collect $:").Format.Alignment = ParagraphAlignment.Right;
                    row15.Cells[4].AddParagraph("Collect $:").Format.Alignment = ParagraphAlignment.Right;
                    section.Add(table6);
                    section.AddParagraph("\n");
                    Table table7 = new Table();
                    table7.Rows.LeftIndent = -53;
                    table7.Borders.Width = 0.5;
                    table7.Format.Font.Name = "Calibri";
                    table7.Format.Font.Size = 11;
                    table7.Borders.Visible = false;
                    column1 = table7.AddColumn(Unit.FromCentimeter(19.5));
                    table7.AddRow().Cells[0].AddParagraph("Received subject to the classifications and tariffs in effect on the date of the issue of the Bill of Lading, the \nproperty described above in apparent good order, except as noted (contents and condition of contents of \npackages unknown), marked, consigned, and destined as indicated above which said carrier (the word carrier \nbeing understood throughout this contract as meaning any person or corporation in possession of the property \nunder the contract) agrees to carry to its usual place of delivery at said destination, if on its route, otherwise to \ndelivery to another carrier on the route to said destination.  It is mutually agreed as to each carrier of all or any \nof, said property over all or any portion of said route to destination and as to each party at any time interested in \nall or any said property, that every service to be performed hereunder shall be to all the bill of lading terms and \nconditions in the governing classification and the said terms and conditions are hereby agreed to by the shipper \nand accepted for himself and his assigns.");
                    section.Add(table7);
                    section.AddParagraph("\n\n");
                    Table table8 = new Table();
                    table8.Rows.LeftIndent = -53;
                    table8.Borders.Width = 0.5;
                    table8.Format.Font.Name = "Calibri";
                    table8.Format.Font.Size = 11;
                    table8.Rows.Height = Unit.FromCentimeter(0.9);
                    column1 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    column1 = table8.AddColumn(Unit.FromCentimeter(6.0));
                    column1 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    Column column2 = table8.AddColumn(Unit.FromCentimeter(4.0));
                    column2.Borders.Left.Visible = false;
                    column2.Borders.Right.Visible = false;
                    Column column3 = table8.AddColumn(Unit.FromCentimeter(2.0));
                    column3.Borders.Left.Visible = false;
                    column3.Borders.Right.Visible = false;
                    column1 = table8.AddColumn(Unit.FromCentimeter(3.0));
                    Row row16 = table8.AddRow();
                    row16.Cells[0].AddParagraph("Shipper:").Format.Alignment = ParagraphAlignment.Right;
                    row16.Cells[0].Format.Borders.Bottom.Visible = false;
                    row16.Cells[1].AddParagraph("AFC Corp.").Format.Font.Bold = true;
                    row16.Cells[0].Format.Borders.Bottom.Visible = false;
                    row16.Cells[2].AddParagraph("Carrier:").Format.Alignment = ParagraphAlignment.Right;
                    row16.Cells[2].Format.Borders.Bottom.Visible = false;
                    row16.Cells[3].Format.Borders.Bottom.Visible = false;
                    Row row17 = table8.AddRow();
                    row17.Cells[0].AddParagraph("Per:").Format.Alignment = ParagraphAlignment.Right;
                    row17.Cells[2].AddParagraph("Per:").Format.Alignment = ParagraphAlignment.Right;
                    row17.Cells[4].AddParagraph("Date:").Format.Alignment = ParagraphAlignment.Right;
                    section.Add(table8);
                    section.Footers.Primary.AddParagraph("Page ").AddPageField();
                    ++num;
                }
                PdfDocumentRenderer documentRenderer = new PdfDocumentRenderer(false);
                documentRenderer.Document = document;
                documentRenderer.RenderDocument();
                documentRenderer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Distribution_Pdf.BillofLading()");
            }
        }

        private class Lot
        {
            public string ItemNo { get; set; } = string.Empty;
            public int ItemLineSeq { get; set; } = 0;
            public string Number { get; set; } = string.Empty;
            public int Qty { get; set; } = 0;
        }
    }
}