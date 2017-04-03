using System;
using System.IO;
using System.Web;
using System.Text;

namespace viviLib.WebComponents.UrlManager
{	/// <summary>
	/// Filter 的摘要说明。
	/// </summary>
	public class Filter : Stream 
	{
		private Stream _sink;
		private long _position;
		private string filePath;
		//		private FileStream _tempFileStream;
		private MemoryStream _tempMemoryStream;
		private BinaryWriter _writer;
		byte[] _buffer;
		int _count;

		public Filter(Stream sink, string file) 
		{
			_sink = sink;
			filePath = file;
		}

		public override bool CanRead 
		{
			get { return true; }
		}

		public override bool CanSeek 
		{
			get { return true; }
		}

		public override bool CanWrite 
		{
			get { return true; }
		}

		public override long Length 
		{
			get { return 0; }
		}

		public override long Position 
		{
			get { return _position; }
			set { _position = value; }
		}

		public override long Seek(long offset, System.IO.SeekOrigin direction) 
		{
			return _sink.Seek(offset, direction);
		}

		public override void SetLength(long length) 
		{
			_sink.SetLength(length);
		}

		public override void Close() 
		{
			try
			{
				if(_sink != null)
				{
					_sink.Close();
				}
				//			if(_tempFileStream != null) _tempFileStream.Close();
				if (this._tempMemoryStream != null)
				{
					if (System.IO.File.Exists(this.filePath))
					{
						viviLib.IO.File.Delete(this.filePath);
					}
					using (FileStream fs	= new FileStream(this.filePath , FileMode.Create , FileAccess.Write , FileShare.None))
					{
						this._tempMemoryStream.WriteTo(fs);
						fs.Close();
					}

					this._tempMemoryStream.Close();
				}
				if(_writer != null)
				{
					_writer.Close();
				}
			}
			catch (Exception ex)
			{
				viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
			}
			finally
			{
//				if (HandlerBase.WritingStaticFilePathes.Contains(this.filePath.ToLower()))
//				{
//					lock (HandlerBase.WritingStaticFilePathes)
//					{
//						HandlerBase.WritingStaticFilePathes.Remove(this.filePath.ToLower());
//					}
//				}
			}
		}

		public override void Flush() 
		{
			_sink.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count) 
		{
			return _sink.Read(buffer, offset, count);
		}

		//		private FileStream TempFileStream
		//		{
		//			get
		//			{
		//				if(_tempFileStream == null)
		//				{
		//					_tempFileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
		//				}
		//				return _tempFileStream;
		//			}
		//		}
		private MemoryStream TempMemoryStream
		{
			get
			{
				if (this._tempMemoryStream == null)
				{
					this._tempMemoryStream	= new MemoryStream();
				}
				return this._tempMemoryStream;
			}
		}
 
		private BinaryWriter BWriter
		{
			get
			{
				if (_writer == null)
				{
					string fileDirectory	= Path.GetDirectoryName(filePath);
					if ( !Directory.Exists(fileDirectory))
					{
						Directory.CreateDirectory(fileDirectory);
					}
//					_writer = new BinaryWriter( TempFileStream );
					this._writer	= new BinaryWriter(this.TempMemoryStream);
				}
				return _writer;
			}
		}

		public override void Write(byte[] buffer, int offset, int count) 
		{
			if (HttpContext.Current.Response.ContentType == "text/html" ||
				HttpContext.Current.Response.ContentType == "text/javascript" ||
				HttpContext.Current.Response.ContentType == "text/vbscript" ||
				HttpContext.Current.Response.ContentType == "text/ecmascript" ||
				HttpContext.Current.Response.ContentType == "text/Jscript" ||
				HttpContext.Current.Response.ContentType == "text/xml") 
			{
				Encoding e = Encoding.GetEncoding(HttpContext.Current.Response.Charset);
				string pageStr = e.GetString(buffer,offset,count);
				_buffer = e.GetBytes(pageStr);
				_count = e.GetByteCount(pageStr);
				_sink.Write(_buffer, 0, _count);
				BWriter.Write(_buffer, 0, _count);
			}
			else 
			{
				_sink.Write(buffer, offset, count);
			}
		}
	}
}
