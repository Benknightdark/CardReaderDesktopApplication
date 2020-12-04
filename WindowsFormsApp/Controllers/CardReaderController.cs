using Microsoft.AspNetCore.Mvc;
using PCSC;
using PCSC.Iso7816;
using System;
using System.Linq;
using System.Text;

namespace WindowsFormsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardReaderController : ControllerBase
    {
        /// <summary>
        /// 取得使用者的健保卡資料
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCardInfo()
        {
            try
            {
                using (var ctx = ContextFactory.Instance.Establish(SCardScope.User))
                {
                    var firstReader = ctx
                        .GetReaders()
                        .FirstOrDefault();

                    if (firstReader == null)
                    {
                        System.Diagnostics.Debug.WriteLine("No reader connected.");
                        return BadRequest("No reader connected.");
                    }

                    using (var isoReader = new IsoReader(context: ctx, readerName: firstReader, mode: SCardShareMode.Shared, protocol: SCardProtocol.Any))
                    {
                        var selectApdu = new CommandApdu(IsoCase.Case4Short, isoReader.ActiveProtocol)
                        {
                            CLA = 0x00,
                            INS = 0xA4,
                            P1 = 0x04,
                            P2 = 0x00,
                            Data = new byte[] { 0xD1, 0x58, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11 },
                            Le = 0x00
                        };

                        System.Diagnostics.Debug.WriteLine("Send Select APDU command: \r\n{0}", BitConverter.ToString(selectApdu.ToArray()));

                        var selectResponse = isoReader.Transmit(selectApdu);
                        System.Diagnostics.Debug.WriteLine("SW1 SW2 = {0:X2} {1:X2}", selectResponse.SW1, selectResponse.SW2);

                        var readProfileApdu = new CommandApdu(IsoCase.Case4Short, isoReader.ActiveProtocol)
                        {
                            CLA = 0x00,
                            INS = 0xCA,
                            P1 = 0x11,
                            P2 = 0x00,
                            Data = new byte[] { 0x00, 0x00 },
                            Le = 0x00
                        };

                        System.Diagnostics.Debug.WriteLine("Send Read Profile APDU command: \r\n{0}", BitConverter.ToString(readProfileApdu.ToArray()));

                        var profileResponse = isoReader.Transmit(readProfileApdu);
                        System.Diagnostics.Debug.WriteLine("SW1 SW2 = {0:X2} {1:X2}", profileResponse.SW1, profileResponse.SW2);

                        if (profileResponse.HasData)
                        {
                            var data = profileResponse.GetData();
                            var cardNumber = GetCardNumber(data[..12]);
                            var cardHolderName = GetHolderName(data[12..32]);
                            var holderIdNum = GetHolderIdNum(data[32..42]);
                            var holderBirth = GetHolderBirthStr(data[42..49]);
                            var holderSex = GetGender(data[49..50]);
                            var cardIssueDate = GetIssueDate(data[50..57]);

                            var CardData = new
                            {
                                cardNumber = cardNumber,
                                holderIdNum = holderIdNum,
                                holderSex = holderSex,
                                holderBirth = holderBirth,
                                cardHolderName = cardHolderName,
                                cardIssueDate = cardIssueDate
                            };
                            return Ok(CardData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("fukc");
        }

        #region Custom Function
        private static string GetIssueDate(byte[] input)
        {
            var asciiEncoding = Encoding.ASCII;
            return asciiEncoding.GetString(input);
        }

        private static string GetGender(byte[] input)
        {
            var asciiEncoding = Encoding.ASCII;
            return asciiEncoding.GetString(input);
        }

        private static string GetHolderBirthStr(byte[] input)
        {
            var asciiEncoding = Encoding.ASCII;
            return asciiEncoding.GetString(input);
        }

        private static string GetHolderIdNum(byte[] input)
        {
            var asciiEncoding = Encoding.ASCII;
            return asciiEncoding.GetString(input);
        }

        private static string GetCardNumber(byte[] input)
        {
            var asciiEncoding = Encoding.ASCII;
            return asciiEncoding.GetString(input);
        }

        private static string GetHolderName(byte[] input)
        {
            string holderName;

            var big5EncodingInfo = Encoding.GetEncodings().FirstOrDefault(_ => _.Name == "big5");

            if (big5EncodingInfo == null)
            {
                // Register a Big5 coding provider
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var encoding = Encoding.GetEncoding("big5");

                holderName = encoding.GetString(input);
            }
            else
            {
                holderName = big5EncodingInfo.GetEncoding().GetString(input);
            }

            return holderName;
        }
        #endregion Custom Function
    }
}