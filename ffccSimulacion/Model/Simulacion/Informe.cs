using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SimuRails.Model.Entities;
using System.Collections.Generic;
using iTextSharp.text.pdf.draw;

namespace SimuRails.Model.Simulacion
{
    public class Informe
    {
        public static string dateString = @"d/M/yyyy";

        public static string timeString = @"HH:mm:ss";

        static public void generarInforme(ResultadoSimulacion resultados)
        {
            SaveFileDialog svdGuardarPdf = new SaveFileDialog();
            //svdGuardarPdf.InitialDirectory = @"C:";
            svdGuardarPdf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            svdGuardarPdf.Title = "Guardar Informe Simulación";
            svdGuardarPdf.DefaultExt = "pdf";
            svdGuardarPdf.Filter = "pdf Files (*.pdf)|*.pdf";
            svdGuardarPdf.FilterIndex = 2;
            svdGuardarPdf.RestoreDirectory = true;
            string nombreArchivo = "";
            if (svdGuardarPdf.ShowDialog() == DialogResult.OK)
            {
                nombreArchivo = svdGuardarPdf.FileName;
            }

            if (nombreArchivo.Trim() != "")
            {
                Document doc = new Document(PageSize.A4, 25, 25, 25, 25);
                FileStream file = new FileStream(nombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                PdfWriter writer = PdfWriter.GetInstance(doc, file);
                // calling PDFFooter class to Include in document
                DateTime dt = DateTime.Now;
                string fecha = dt.ToString(dateString);
                string hora = dt.ToString(timeString);
                writer.PageEvent = new PDFFormatter(resultados.nombreSimulacion, fecha, hora, resultados.tiempoSimulado);
                doc.Open();
                doc = generarEscenario(doc, resultados.idTraza);
                doc = generarResultado(doc, resultados);
                doc.Close();

                //Abre el archivo una vez creado no se si es necesario, pero es un buen feature
                Process.Start(nombreArchivo);
            }
        }

        private static Document generarEscenario(Document doc, int idTraza)
        {
            string escenarioStr;
            char[] charToTrim = { ' ', ',', ')', '.' };

            SimuRailsEntities context = new SimuRailsEntities();

            escenarioStr = context.Trazas.Where(x => x.Id == idTraza).FirstOrDefault().Nombre;
            doc.Add(new Paragraph("Escenario de Simulación\n", FontFactory.GetFont("ARIAL", 13, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph("Traza", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph(escenarioStr, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            Servicios unServicio;
            Formaciones unaFormacion;
            Estaciones unaEstacion;
            escenarioStr = "";
            //doc.Add(new Paragraph("                     -----------"));
            foreach (var ts in context.Trazas_X_Servicios.Where(x => x.Id_Traza == idTraza))
            {
                doc.Add(new Paragraph("Servicios", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
                unServicio = context.Servicios.Where(y => y.Id == ts.Id_Servicio).FirstOrDefault();
                doc.Add(new Paragraph(unServicio.Nombre, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
                doc.Add(new Paragraph("Formación ", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
                foreach (var sf in context.Servicios_X_Formaciones.Where(x => x.Id_Servicio == unServicio.Id))
                {
                    unaFormacion = context.Formaciones.Where(y => y.Id == sf.Id_Formacion).FirstOrDefault();
                    doc.Add(new Paragraph(unaFormacion.NombreFormacion, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
                    doc.Add(new Paragraph("Coches", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
                    escenarioStr = "";
                    foreach (var fc in context.Formaciones_X_Coches.Where(x => x.Id_Formacion == unaFormacion.Id))
                    {
                        escenarioStr += context.Coches.Where(y => y.Id == fc.Id_Coche).FirstOrDefault().Modelo + ", ";
                    }
                    escenarioStr = escenarioStr.TrimEnd(charToTrim);
                }
                escenarioStr = escenarioStr.TrimEnd(charToTrim);
                escenarioStr += ".";
                doc.Add(new Paragraph(escenarioStr, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
                //doc.Add(new Paragraph("                     -----------"));

                doc.Add(new Paragraph("Estaciones", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));
                escenarioStr = "";
                foreach (var r in context.Relaciones.Where(y => y.Id_Servicio == ts.Id_Servicio))
                {
                    unaEstacion = context.Estaciones.Where(y => y.Id == r.Id_Estacion_Anterior).FirstOrDefault();
                    escenarioStr += unaEstacion.Nombre;
                    if (context.Estaciones_X_Incidentes.Any(x => x.Id_Estacion == unaEstacion.Id))
                    {
                        escenarioStr += " (";
                        foreach (var ei in context.Estaciones_X_Incidentes.Where(x => x.Id_Estacion == unaEstacion.Id))
                        {
                            escenarioStr += context.Incidentes.Where(y => y.Id == ei.Id_Incidente).FirstOrDefault().Nombre + ", ";
                        }
                        escenarioStr = escenarioStr.TrimEnd(charToTrim);
                        escenarioStr += ") ";
                    }
                    escenarioStr += ", ";
                }
                escenarioStr = escenarioStr.TrimEnd(charToTrim);
                escenarioStr += ".";
                doc.Add(new Paragraph(escenarioStr, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            }
            return doc;
        }

        private static Document generarResultado(Document doc, ResultadoSimulacion resultados)
        {
            doc.NewPage();
            doc.Add(new Paragraph("Resultados", FontFactory.GetFont("ARIAL", 13, iTextSharp.text.Font.BOLD)));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("Porcentaje de Trenes que Superaron el Máximo de Pasajeros Permitidos:  " + resultados.porcentajeSobrecarga + " %", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            doc.Add(new Paragraph("Tiempo Promedio de Demora Por Incidentes:  " + resultados.promedioDemoraIncidentes + " Minutos", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            doc.Add(new Paragraph("Promedio de Pasajeros Por Formación:  " + resultados.promedioPasajeros + " Pasajeros", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            doc.Add(new Paragraph("Promedio de Demora Por Atención en Estación:  " + resultados.promedioDemoraAtencion + " Minutos", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            doc.Add(new Paragraph("Costo de Formación Por Kilómetro:  " + resultados.costoKm + " $", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));
            doc.Add(new Paragraph("Costo de Formación Por Pasajero:  " + resultados.costoPasajero + " $", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC)));

            int servicios = resultados.resultadosServicios.Count();

            doc.Add(Chunk.NEWLINE);
            Paragraph par = new Paragraph("Resultados por servicio", FontFactory.GetFont("ARIAL", 13, iTextSharp.text.Font.BOLD));
            par.Leading = 10;
            doc.Add(par);
            doc.Add(new Paragraph("\n"));
            generarTabla(servicios, "Porcentaje de Trenes que Superaron el Máximo de Pasajeros Permitidos:  ", resultados.resultadosServicios, new porcentajePasajerosMaximos(), doc, "%");
            generarTabla(servicios, "Promedio de pasajeros por encima del Máximo de Pasajeros Permitidos:  ", resultados.resultadosServicios, new promedioPasajerosMaximos(), doc, " p.");
            generarTabla(servicios, "Tiempo Promedio de Demora Por Incidentes:  ", resultados.resultadosServicios, new promedioDemoraIncidentes(), doc, " min");
            generarTabla(servicios, "Promedio de Pasajeros Por Formación:  ", resultados.resultadosServicios, new promedioPasajerosFormacion(), doc, " p.");
            generarTabla(servicios, "Promedio de Demora Por Atención en Estación:  ", resultados.resultadosServicios, new promedioAtencionEstacion(), doc, " min");
            generarTabla(servicios, "Porcentaje de pasajeros que viajaron parados:  ", resultados.resultadosServicios, new porcentajePasajerosParados(), doc, "%");

            return doc;
        }

        static private void generarTabla(int servicios, string header, List<ResultadoServicio> resultadosServicio, valueGenerator value, Document doc, string unidad)
        {
            PdfPTable table = new PdfPTable(servicios);

            PdfPCell cell = new PdfPCell(new Phrase(header));
            cell.Colspan = servicios;
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            foreach (ResultadoServicio result in resultadosServicio)
            {
                PdfPCell cellNombre = new PdfPCell(new Phrase(result.nombre));
                cellNombre.BackgroundColor = new BaseColor(128, 255, 255, 50);
                cellNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cellNombre);
            }

            foreach (ResultadoServicio result in resultadosServicio)
            {
                PdfPCell promedio = new PdfPCell(new Phrase(Convert.ToString(value.generateValue(result)) + unidad));
                promedio.HorizontalAlignment = Element.ALIGN_CENTER;
                promedio.Padding = 2f;
                table.AddCell(promedio);

            }

            doc.Add(table);

            doc.Add(Chunk.NEWLINE);
        }

    }

    public class PDFFormatter : PdfPageEventHelper
    {
        private string nombreSimulacion;
        private string fecha;
        private string hora;
        private int tiempoTotal;

        public PDFFormatter(string nombreSimulacion, string fecha, string hora, int tiempoTotal)
        {
            this.nombreSimulacion = nombreSimulacion;
            this.fecha = fecha;
            this.hora = hora;
            this.tiempoTotal = tiempoTotal;
        }

        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            string path = Application.StartupPath + "\\Resources\\logo.png";
            Image logo = Image.GetInstance(new Uri(path));
            logo.ScalePercent(30);
            logo.Alignment = Image.TEXTWRAP | Image.ALIGN_LEFT;
            string encabezadoStr = string.Format("Simulador FFCC\nNombre de Simulación: {0}         Duración:{3}         Fecha: {1}         Hora: {2}", nombreSimulacion, fecha, hora, tiempoTotal);
            Paragraph encabezado = new Paragraph(encabezadoStr, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLDITALIC));
            document.Add(logo);
            document.Add(encabezado);
            Paragraph p = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();

            ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, new Paragraph("SIMULADOR FFCC v2 2017 - UTN FRBA. Todos los derechos Reservados.", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)),
                (document.Right - document.Left) / 2 + document.LeftMargin,
                document.Bottom + 10, 0);

        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }

    abstract public class valueGenerator
    {
        abstract public double generateValue(ResultadoServicio result);
    }

    public class porcentajePasajerosMaximos : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            return Math.Round((double)result.vecesSuperoCapLegal * 100 / ((double) result.resultadosFormaciones.Count()), 2);
        }

    }

    public class promedioPasajerosMaximos : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            return Math.Round((double)result.totalSobrecargaLegal / ((double) ( result.resultadosFormaciones.Count()) * result.cantidadEstaciones), 2);
        }

    }

    public class promedioDemoraIncidentes : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            {
                return Math.Round((double)result.tiempoTotalDemoradoIncidente / ((double)(result.resultadosFormaciones.Count())), 2);
            }
        }

    }

    public class promedioPasajerosFormacion : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            return Math.Truncate((double)result.pasajerosTotalesTransportados / (double)result.resultadosFormaciones.Count());
        }

    }

    public class promedioAtencionEstacion : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            return Math.Round((double)result.tiempoTotalDemoradoAtencion / (double)(result.resultadosFormaciones.Count()), 2);
        }

    }

    public class porcentajePasajerosParados : valueGenerator
    {
        public override double generateValue(ResultadoServicio result)
        {
            return Math.Round((double)result.totalPasajerosParados * 100 / (double) result.pasajerosTotalesTransportados, 2);
        }

    }
}
