using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace ReadBooks.Models
{
    public class IndustryIdentifier
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    public class ReadingModes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    public class PanelizationSummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    public class VolumeInfo
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public List<string> authors { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public List<IndustryIdentifier> industryIdentifiers { get; set; }
        public ReadingModes readingModes { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        public List<string> categories { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public PanelizationSummary panelizationSummary { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
        public ImageLinks imageLinks { get; set; }
        public string publisher { get; set; }
        public double? averageRating { get; set; }
        public int? ratingsCount { get; set; }
    }

    public class ListPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class RetailPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class Offer
    {
        public int finskyOfferType { get; set; }
        public ListPrice listPrice { get; set; }
        public RetailPrice retailPrice { get; set; }
    }

    public class SaleInfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
        public ListPrice listPrice { get; set; }
        public RetailPrice retailPrice { get; set; }
        public string buyLink { get; set; }
        public List<Offer> offers { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class AccessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    public class SearchInfo
    {
        public string textSnippet { get; set; }
    }

    public class Item
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private VolumeInfo _volumeInfo;
        [Ignore]
        public VolumeInfo volumeInfo
        {
            get
            {
                return _volumeInfo;
            }
            set
            {
                _volumeInfo = value;

                title = _volumeInfo.title;
                publisher = _volumeInfo.publisher;
                publishedDate = _volumeInfo.publishedDate;
                if (_volumeInfo.authors != null)
                {
                    foreach (var author in _volumeInfo.authors)
                        authors += author + ", ";
                }
                if (_volumeInfo.imageLinks != null)
                    thumbnail = _volumeInfo.imageLinks.thumbnail;
            }
        }

        [Ignore]
        public string kind { get; set; }
        [Ignore]
        public string id { get; set; }
        [Ignore]
        public string etag { get; set; }
        [Ignore]
        public string selfLink { get; set; }
        [Ignore]
        public SaleInfo saleInfo { get; set; }
        [Ignore]
        public AccessInfo accessInfo { get; set; }
        [Ignore]
        public SearchInfo searchInfo { get; set; }

        public string title { get; set; }
        public string authors { get; set; }
        public string thumbnail { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
    }

    public class BooksAPI
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public List<Item> items { get; set; }
    }
}
