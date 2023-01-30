# Database-Management-Systems-Project
# Problem Tanımı
+ Müşterileri, araç sahipleriyle bir araya getirip ulaşım sağlama amacını gerçekleştiren uygulamanın veri 
tabanının gerçeklenmesi.

# İş Kuralları (Business Rules)
+ Kullanıcı tablosunda isim, soy isim, e-mail, telefon no, yaş, kullanıcı türü, kullanıcı adı, şifre
bilgileri mevcuttur.
+ Kullanıcı müşteri veya sürücü olmak zorundadır. Aynı anda ikisi de olamaz.
+ Adres tablosunda ilid, ilçeid, kullanıcıid ve açık adres bilgileri mevcuttur.
+ Bir adres en çok bir en az sıfır kullanıcıya ait olabilir. Bir kullanıcı en çok bir en az bir adrese 
sahip olur.
+ İl tablosunda il adı bilgisi mevcuttur.
+ Bir il en çok çok en az sıfır adreste bulunur. Bir adreste en çok bir en az bir il bulunur.
+ İlçe tablosunda ilçe adı ve il id bilgileri mevcuttur.
+ Bir ilçe en çok çok en az sıfır adreste bulunur. Bir adreste en çok bir en az bir ilçe bulunur.
+ Bir ilçe en çok bir en az bir ile ait olabilir. Bir ilde en çok en az bir ilçe bulunur.
+ Sürücü tablosunda sürücü id bulunur.
+ Vardiya tablosunda tarih, sürücüid, bitiş zamanı, başlama zamanı bilgileri mevcuttur.
+ Bir sürücünün en çok çok en sıfır vardiyası bulunabilir. Bir vardiya en çok bir en az bir 
 sürücüye ait olabilir.
+ Araç tablosunda model yılı, renk, kapasite ve sürücü id bilgileri mevcuttur.
+ Bir sürücünün en çok bir en az bir aracı bulunabilir. Bir araç en çok bir en az bir sürücüye 
aittir.
+ Sigorta tablosunda bitiş tarihi ve araç id bilgileri mevcuttur.
+ Bir aracın en çok bir en az sıfır sigortası bulunur. Bir sigorta en çok bir en az bir araca ait 
olabilir.
+ Ehliyet tablosunda bitiş tarihi ve sürücü id bilgileri mevcuttur.
+ Bir sürücünün en çok bir en az bir ehliyeti olabilir. Bir ehliyet en çok bir en az bir sürücüye ait 
olabilir.
+ Kart tablosunda cvv, bitiş tarihi, kart numarası, fatura adresi ve müşteri id bilgileri mevcuttur.
+ Bir kart en çok bir en az bir adres içerebilir. Bir adres en çok çok en az sıfır karta ait olabilir.
+ Bir kart en çok bir en az bir müşteriye ait olabilir. Bir müşterinin en çok bir en az sıfır kartı 
olabilir.
+ Seyahat isteği tablosunda gidilecek konum, binilecek konum, müşteri id ve sürücü id bilgileri 
mevcuttur.
+ Seyahat isteği tamamlanmış veya tamamlanmamış seyahat olmak zorundadır.
+ Bir müşteri en çok çok en az sıfır seyahat isteğinde bulunabilir. Bir seyahat isteği en çok bir en 
az bir müşteriye aittir.
+ Bir sürücü aynı anda en çok bir en az sıfır seyahat isteği kabul edebilir. Seyahat isteği en çok 
bir en az sıfır sürücü tarafından kabul edilebilir.
+ Oluşan seyahat istekleri direkt olarak tamamlanmamış seyahat tablosuna eklenir, seyahat 
isteği kabul edildiğinde tamamlanmamış seyahat tablosundan tamamlanmış seyahat 
tablosuna taşınır.
+ Fatura tablosunda süre, bahşiş, kart id, ücret ve tamamlanmış id bilgileri mevcuttur.
+ Bir fatura en çok bir en az bir tamamlanmış seyahate ait olabilir. Bir tamamlanmış seyahat en 
çok bir en az bir fatura içerir.
+ Bir kart en çok çok en az sıfır faturada kullanılabilir. Bir faturada en çok bir en az bir kart 
kullanılabilir.
+ Eski kullanıcı tablosunda isim, soy isim, e-mail, telefon numarası, yaş, kullanıcı türü, kullanıcı 
adı ve şifre bilgileri mevcuttur.
+ Eski kullanıcı tablosuna silinen kullanıcılar otomatik olarak eklenir.
+ Eski araç tablosunda model yılı, renk, araç id, kapasite ve sürücü id bilgileri mevcuttur.
+ Eski araç tablosuna üzerinde güncelleme yapılan araçlar otomatik olarak eklenir.
# İlişkisel Şema
+ adres(adresid: serial, acikadres: varchar(1000), ilid: integer, ilceid: integer, kullaniciid: integer)
+ araç(aracid: serial, modelyili: smallint, renk: varchar(15), kapasite: smallint, surucuid: integer)
+ ehliyet(ehliyetid: serial, bitistarihi: date, surucuid: integer)
+ eskiarac(eskiaracid: serial, aracid: integer, modelyili: smallint, renk: varchar(15), kapasite: 
smallint, surucuid: integer)
+ eskikullanici(eskikullaniciid: serial, isim: varchar(50), soyisim: varchar(50), email: varchar(50), 
telefonno: varchar(50), yas: smallint, kullanicituru: varchar(1), kullaniciadi: varchar(50), sifre: 
varchar(50))
+ fatura(faturaid: serial, kartid: integer, tamamlamisid: integer, sure: varchar(40), bahsis: 
money, ucret: money)
+ il(ilid: serial, iladi: varchar(50))
+ ilce(ilceid: serial, ilceadi: varchar(50), ilid: integer)
+ kart(kartid: serial, cvv: smallint, bitistarihi: date, kartnumarasi: varchar(16), faturaadresi: 
integer, musteriid: integer)
+ kullanici(kullaniciid: serial, isim: varchar(30), soyisim: varchar(30), email: varchar(50), 
telefonno: varchar(20), yas: smallint, kullanicitipi: varchar(1), kullaniciadi: varchar(20), sifre: 
varchar(20))
+ müşteri(musteriid: integer, musteritipi: varchar(30), isteksayac: integer)
+ seyehatistegi(istekid: serial, musteriid: integer, surucuid: integer, gidilecekkonum: 
varchar(100), binilecekkonum: varchar(100))
+ sigorta(sigortaid: serial, aracid: integer, bitistarihi: date)
+ surucu(surucuid: integer)
+ tamamlanmamisseyahat(tamamlanmamisid: integer, rezervasyontarihi: date)
+ tamamlanmisseyahat(tamamlanmisid: integer)
+ vardiya(vardiyaid: serial, tarih: date, bitiszamani: time without timezone, baslangiczamani: : 
time without timezone, surucuid: integer)


![database](https://user-images.githubusercontent.com/95939881/215474500-f917c6d1-6c13-4709-a4fb-63b4c883675f.png)

# Saklı Yordam(Stored Procedure)
+ EHLİYETSİL: kullanım süresi dolan ehliyetleri siler
+ İLCEEKLE: ilce eklemeye yarar
+ İLEKLE: il eklemeye yarar
+ MUSTERİSİL: 18 yaşından küçük müşterileri siler

# Tetikleyiciler(Triggers)
+ ESKIARAC: güncellenen araçların eski halini eski araç tablosuna ekler
+ ESKIKULLANICI: silinen kullanıcıyı eskikullanici tablosuna ekler
+ ISTEKSAYACARTTIR: müşteri seyahat isteği oluşturduğunda sayacını artırır
+ KAYITTARIHI: kullanıcının kayıt tarihini ekler
