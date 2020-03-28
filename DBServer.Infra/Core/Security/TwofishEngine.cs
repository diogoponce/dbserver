using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace DBServer.Infra.Core.Security
{
    public class CriptoParameter
    {
        public byte[] Salt { get; set; }
        public string Data { get; set; }
        public string Password { get; set; }
    }

    public class TwofishEngine
    {
        Encoding _encoding;
        IBlockCipher _blockCipher;
        private PaddedBufferedBlockCipher _cipher;
        private IBlockCipherPadding _padding;
        KeyParameter KeyParameter;

        #region Public Methods

        public static TwofishEngine Instance
        {
            get
            {
                return new TwofishEngine();
            }
        }

        public string Encrypt(CriptoParameter param)
        {
            if (param is null)
                throw new ArgumentException("");

            TwoFish(param.Password, param.Salt);
            return Encrypt(param.Data, KeyParameter);
        }

        public string Decrypt(CriptoParameter param)
        {
            if (param is null)
                throw new ArgumentException("");

            TwoFish(param.Password, param.Salt);
            return Decrypt(param.Data, KeyParameter);
        }

        #endregion

        #region Private Methods

        void TwoFish(string Password, byte[] Salt)
        {
            Sha3Digest Sha3Digest = new Sha3Digest(256);
            Pkcs5S2ParametersGenerator gen = new Pkcs5S2ParametersGenerator(Sha3Digest);
            gen.Init(Encoding.UTF8.GetBytes(Password), Salt, 1000);
            KeyParameter = (KeyParameter)gen.GenerateDerivedParameters(CryptoEngines.TwofishEngine.AlgorithmName, 256);

            SetPadding(new Pkcs7Padding());
            SetBlockCipher(CryptoEngines.TwofishEngine);
            SetEncoding(Encoding.UTF8);
        }

        void SetPadding(IBlockCipherPadding padding)
        {
            if (padding != null)
                _padding = padding;
            else
                throw new NullReferenceException(message: "Padding is null!");
        }

        void SetBlockCipher(IBlockCipher blockCipher)
        {
            if (blockCipher != null)
                _blockCipher = blockCipher;
            else
                throw new NullReferenceException(message: "BlockCipher is null!");
        }

        void SetEncoding(Encoding encoding)
        {
            if (encoding != null)
                _encoding = encoding;
            else
                throw new NullReferenceException(message: "Encoding is null!");
        }

        string Encrypt(string plain, ICipherParameters SetKeyParameter)
        {
            byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(plain), SetKeyParameter);
            return Convert.ToBase64String(result);
        }

        string Decrypt(string cipher, ICipherParameters SetKeyParameter)
        {
            byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), SetKeyParameter);
            return _encoding.GetString(result, 0, result.Length);
        }

        byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, ICipherParameters SetKeyParameter)
        {
            try
            {
                _cipher = _padding == null
                    ? new PaddedBufferedBlockCipher(_blockCipher)
                    : new PaddedBufferedBlockCipher(_blockCipher, _padding);

                _cipher.Init(forEncrypt, SetKeyParameter);

                byte[] ret = _cipher.DoFinal(input);
                return ret;

            }
            catch
            {
                throw;
            }
        }

        #endregion

    }

    public sealed class CryptoEngines
    {
        static AesEngine _AesEngine;
        public static AesEngine AesEngine
        {
            get
            {
                if (_AesEngine == null)
                    _AesEngine = new AesEngine();

                return _AesEngine;
            }
        }

        static AesLightEngine _AesLightEngine;
        public static AesLightEngine AesLightEngine
        {
            get
            {
                if (_AesLightEngine == null)
                    _AesLightEngine = new AesLightEngine();

                return _AesLightEngine;
            }
        }

        static BlowfishEngine _BlowfishEngine;
        public static BlowfishEngine BlowfishEngine
        {
            get
            {
                if (_BlowfishEngine == null)
                    _BlowfishEngine = new BlowfishEngine();

                return _BlowfishEngine;
            }
        }

        static CamelliaEngine _CamelliaEngine;
        public static CamelliaEngine CamelliaEngine
        {
            get
            {
                if (_CamelliaEngine == null)
                    _CamelliaEngine = new CamelliaEngine();

                return _CamelliaEngine;
            }
        }

        static CamelliaLightEngine _CamelliaLightEngine;
        public static CamelliaLightEngine CamelliaLightEngine
        {
            get
            {
                if (_CamelliaLightEngine == null)
                    _CamelliaLightEngine = new CamelliaLightEngine();

                return _CamelliaLightEngine;
            }
        }

        static Cast5Engine _Cast5Engine;
        public static Cast5Engine Cast5Engine
        {
            get
            {
                if (_Cast5Engine == null)
                    _Cast5Engine = new Cast5Engine();

                return _Cast5Engine;
            }
        }

        static DesEngine _DesEngine;
        public static DesEngine DesEngine
        {
            get
            {
                if (_DesEngine == null)
                    _DesEngine = new DesEngine();

                return _DesEngine;
            }
        }

        static Gost28147Engine _Gost28147Engine;
        public static Gost28147Engine Gost28147Engine
        {
            get
            {
                if (_Gost28147Engine == null)
                    _Gost28147Engine = new Gost28147Engine();

                return _Gost28147Engine;
            }
        }

        static IdeaEngine _IdeaEngine;
        public static IdeaEngine IdeaEngine
        {
            get
            {
                if (_IdeaEngine == null)
                    _IdeaEngine = new IdeaEngine();

                return _IdeaEngine;
            }
        }

        static NoekeonEngine _NoekeonEngine;
        public static NoekeonEngine NoekeonEngine
        {
            get
            {
                if (_NoekeonEngine == null)
                    _NoekeonEngine = new NoekeonEngine();

                return _NoekeonEngine;
            }
        }

        static NullEngine _NullEngine;
        public static NullEngine NullEngine
        {
            get
            {
                if (_NullEngine == null)
                    _NullEngine = new NullEngine();

                return _NullEngine;
            }
        }

        static RC2Engine _RC2Engine;
        public static RC2Engine RC2Engine
        {
            get
            {
                if (_RC2Engine == null)
                    _RC2Engine = new RC2Engine();

                return _RC2Engine;
            }
        }

        static RC532Engine _RC532Engine;
        public static RC532Engine RC532Engine
        {
            get
            {
                if (_RC532Engine == null)
                    _RC532Engine = new RC532Engine();

                return _RC532Engine;
            }
        }

        static RC564Engine _RC564Engine;
        public static RC564Engine RC564Engine
        {
            get
            {
                if (_RC564Engine == null)
                    _RC564Engine = new RC564Engine();

                return _RC564Engine;
            }
        }

        static RC6Engine _RC6Engine;
        public static RC6Engine RC6Engine
        {
            get
            {
                if (_RC6Engine == null)
                    _RC6Engine = new RC6Engine();

                return _RC6Engine;
            }
        }

        static RijndaelEngine _RijndaelEngine;
        public static RijndaelEngine RijndaelEngine
        {
            get
            {
                if (_RijndaelEngine == null)
                    _RijndaelEngine = new RijndaelEngine(256);

                return _RijndaelEngine;
            }
        }

        static SeedEngine _SeedEngine;
        public static SeedEngine SeedEngine
        {
            get
            {
                if (_SeedEngine == null)
                    _SeedEngine = new SeedEngine();

                return _SeedEngine;
            }
        }

        static SerpentEngine _SerpentEngine;
        public static SerpentEngine SerpentEngine
        {
            get
            {
                if (_SerpentEngine == null)
                    _SerpentEngine = new SerpentEngine();

                return _SerpentEngine;
            }
        }

        static SkipjackEngine _SkipjackEngine;
        public static SkipjackEngine SkipjackEngine
        {
            get
            {
                if (_SkipjackEngine == null)
                    _SkipjackEngine = new SkipjackEngine();

                return _SkipjackEngine;
            }
        }

        static TeaEngine _TeaEngine;
        public static TeaEngine TeaEngine
        {
            get
            {
                if (_TeaEngine == null)
                    _TeaEngine = new TeaEngine();

                return _TeaEngine;
            }
        }

        static ThreefishEngine _ThreefishEngine256;
        public static ThreefishEngine ThreefishEngine256
        {
            get
            {
                if (_ThreefishEngine256 == null)
                    _ThreefishEngine256 = new ThreefishEngine(ThreefishEngine.BLOCKSIZE_256);

                return _ThreefishEngine256;
            }
        }

        static ThreefishEngine _ThreefishEngine512;
        public static ThreefishEngine ThreefishEngine512
        {
            get
            {
                if (_ThreefishEngine512 == null)
                    _ThreefishEngine512 = new ThreefishEngine(ThreefishEngine.BLOCKSIZE_512);

                return _ThreefishEngine512;
            }
        }

        static ThreefishEngine _ThreefishEngine1024;
        public static ThreefishEngine ThreefishEngine1024
        {
            get
            {
                if (_ThreefishEngine1024 == null)
                    _ThreefishEngine1024 = new ThreefishEngine(ThreefishEngine.BLOCKSIZE_1024);

                return _ThreefishEngine1024;
            }
        }

        static TnepresEngine _TnepresEngine;
        public static TnepresEngine TnepresEngine
        {
            get
            {
                if (_TnepresEngine == null)
                    _TnepresEngine = new TnepresEngine();

                return _TnepresEngine;
            }
        }

        static Org.BouncyCastle.Crypto.Engines.TwofishEngine _TwofishEngine;
        public static Org.BouncyCastle.Crypto.Engines.TwofishEngine TwofishEngine
        {
            get
            {
                if (_TwofishEngine == null)
                    _TwofishEngine = new Org.BouncyCastle.Crypto.Engines.TwofishEngine();

                return _TwofishEngine;
            }
        }

        static XteaEngine _XteaEngine;
        public static XteaEngine XteaEngine
        {
            get
            {
                if (_XteaEngine == null)
                    _XteaEngine = new XteaEngine();

                return _XteaEngine;
            }
        }
    }
}
