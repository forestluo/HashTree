using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public interface IFileContainer
    {
        public long GetLength();

        public long GetDataSize();

        public long GetPosition();

        public void SetPosition(long position);

        public int ReadInteger(long position);

        public void WriteInteger(long position, int value);

        public void ReadFully(long position, byte[] bytes);

        public void WriteFully(long position, byte[] bytes);

        public void ReadBytes(long position, byte[] bytes, int offset, int length);

        public void WriteBytes(long position, byte[] bytes, int offset, int length);

        public void ReadFully(long position, SimpleBuffer buffer);

        public void WriteFully(long position, SimpleBuffer buffer);

        public void ReadPartially(long position, SimpleBuffer buffer, int length);

        public void WritePartially(long position, SimpleBuffer buffer, int length);

        public void ReadFully(long position, PageDescription description);

        public void WriteFully(long position, PageDescription description);

        public void ReadFully(long position, PageBuffer pageBuffer);

        public void WriteFully(long position, PageBuffer pageBuffer);

        public void ReadPartially(long position, PageBuffer pageBuffer, int length);

        public void WritePartially(long position, PageBuffer pageBuffer, int length);
    }
}
