using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace proje3

{   

    class Müşteri
    {
        public int müşteriID = 0;

        public int saat = 0;
        public int dakika = 0;
        public Müşteri() {
            Random random = new Random();
            this.saat = random.Next(0, 24);
            this.dakika = random.Next(0, 60);
            this.müşteriID = random.Next(1,21);
        }
        public Müşteri(int ID)
        {
            this.müşteriID = ID;

            Random random = new Random();
            this.saat = random.Next(0, 24);
            this.dakika = random.Next(0, 60);
        }
        public String getZaman()
        {
            return saat.ToString() + ":" + dakika.ToString(); ;
        }
        
        public String ToString()
        {
            return müşteriID+" "+getZaman();
        }
       
    }
    class Durak
    {
        public String durakAdı = "";
        public int boşPark = 0;
        public int tandemBisiklet = 0;
        public int normalBisiklet = 0;
        public List<Müşteri> müşteriler;

        public Durak(String nam,int par,int tan,int bis)
        {
            this.durakAdı = nam;
            this.boşPark = par;
            this.tandemBisiklet = tan;
            this.normalBisiklet = bis;
            this.müşteriler = new List<Müşteri>();
        }

        public String ToString()
        {
            return durakAdı+" "+boşPark+" "+tandemBisiklet+" "+normalBisiklet;
        }
        
        

    }

    class BinarySearchTree{

        public Node root;
 
        internal class Node
        {
            internal Durak durak;
            internal Node parent;
            internal Node leftChild;
            internal Node rightChild;
            internal Node()
            {
                this.durak    =null;
                this.rightChild = null;
                this.leftChild= null;
                this.parent = null;
            }

        }
        public BinarySearchTree()
        {
            this.root = new Node();
        }
        public void AddToTree(Durak dur) {//büyük ise sağ çocuk
                                //recursive de yazıalbilir

            Node eklenecekNode = new Node();
            eklenecekNode.durak = dur;
            if (root.durak!=null)//kök boş değilse girilen eleman ağaç içerisinde gezilerek eklenmelidir.
            {
                Node tempNode = root;
                while (true)
                {
                        
                    if (String.Compare(tempNode.durak.durakAdı, dur.durakAdı, true, new CultureInfo("tr-TR")) == -1)//-1 ise sağında kalmaktadır
                                                      
                    {
                        if (tempNode.rightChild == null)//sağ çocuk boş ise ekleme buraya yapılmalıdır.
                        {
                            tempNode.rightChild = eklenecekNode;
                            eklenecekNode.parent = tempNode;
                            break;
                        }
                        else//boş olan node bulunana kadar bu kontrol işlemleri devam ettirilmeilidir.
                        {
                            tempNode = tempNode.rightChild;
                        }
                    }
                    else//sol taraf
                    {
                        if (tempNode.leftChild== null)//sol çocuk boş ise ekleme buraya yapılmalıdır.
                        {
                            tempNode.leftChild= eklenecekNode;
                            eklenecekNode.parent = tempNode;
                            break;
                        }
                        else//boş olan node bulunana kadar bu kontrol işlemleri devam ettirilmeilidir.
                        {
                            tempNode = tempNode.leftChild;
                        }
                    }
                }
            }
            else//kök boş ise köke eklenecektir
            {
                root.durak=dur;
            }

        }

        public int DerinlikBul(Node tempNode, int sayı = 0)
        {
            
            if (tempNode!=null)//temp node null olana kadar öncelikle en sol çocuğa ulaşıçak ve null olmadan önceki seviyeyi döndürücek.
                            //en soldaki çocuğun derinliğini sağındaki kardeşinin en derin çoğunun derinliği ile karşılaşıtırıp sırası ile yukarı çıkıcaktır.
            {
                int ikisideEşitGirsin = sayı+1;
                int sol = DerinlikBul(tempNode.leftChild, ikisideEşitGirsin);
                int sağ = DerinlikBul(tempNode.rightChild, ikisideEşitGirsin);
                if (sağ > sol)
                {
                    return sağ;
                }
                return sol;

            }

            return sayı-1;//nulldan önceki derinliğin döndürülmesi
            
        }
        

        public void AğaçYaz(Node tempNode)
        {
            if (tempNode!= null)
            {
                AğaçYaz(tempNode.leftChild);
                AğaçYaz(tempNode.rightChild);
                Console.WriteLine(tempNode.durak.ToString());
                foreach (Müşteri müş in tempNode.durak.müşteriler)
                {
                    Console.WriteLine(müş.ToString());
                }
                Console.WriteLine("---------");


            }
        }
        public void SearchFoID(int id,Node tempNode)//post order treversal 
        {

            if (tempNode!= null)
            {
                SearchFoID(id,tempNode.leftChild);
                SearchFoID(id,tempNode.rightChild);
                
                foreach (Müşteri müş in tempNode.durak.müşteriler)
                {
                    if (müş.müşteriID==id)
                    {
                        Console.WriteLine(tempNode.durak.durakAdı + " saat : " + müş.getZaman());
                    }
                    
                }
            }
        }
        public void Kiralama(String durak,int id)
        {
            Node temp = this.root;
            while (true)
            {
                if (temp!=null) {
                    if (temp.durak != null & durak != null)
                    {

                        int sonuç = String.Compare(temp.durak.durakAdı, durak, true, new CultureInfo("tr-TR"));
                        if (sonuç == -1) {
                            temp = temp.leftChild;
                        }
                        else if (sonuç == 1)
                        {
                            temp = temp.rightChild;
                        }
                        else//doğru durak bulunmuştur
                        {
                            Müşteri müş = new Müşteri(id);
                            temp.durak.müşteriler.Add(müş);
                            temp.durak.boşPark++;
                            temp.durak.normalBisiklet--;
                            break;//bulunduysa devam edilmesine gerek yoktur.
                        }
                    }
                }
                else//bütün ağaç dolaşılıp bulunamadı 
                {
                    Console.WriteLine("bütün ağaç dolaşılıp bulunamadı");
                    break;
                }
            }
        }
    }



    class MaxHeap
    {
        private Durak[] duraklar;
        private int size=0;
        private int maxSize=1;

        public MaxHeap(int mxs = 1) {
            this.maxSize = mxs;
            duraklar = new Durak[maxSize];
        }

        public int Parent(int index) { return index / 2; }
        public int LeftChild(int index) { return index* 2+1 ; }
        public int RightChild(int index) { return index * 2 +2; }

        public Durak getMax() {//en fazla bisiklete sahip nesneyi heapten çıkartıp geri döndürür.
            Durak döndür = duraklar[0];//ilk itemin döndürülmesi

            if (size > 0) {//heap boş olabilir.
                duraklar[0] = duraklar[size-1];//son itemin başa getirilmesi
                duraklar[size-1] = null;//son kullanılmış indexin boşaltılması
                size--;
                int index = 0;//heap gezilirken yardımcı olucak
                while (true)
                {
                    int indexOfLeftChild = LeftChild(index);
                    int indexOfRightChild = RightChild(index);
                    if (indexOfLeftChild < maxSize || indexOfRightChild < maxSize)
                    {
                        if (duraklar[LeftChild(index)] != null)//sol çocuk boş mu 
                        {
                            if (duraklar[RightChild(index)] != null)//eğer sağ çocukta boş değilse ikisinden büyük olan ile parent değiştirilmelidir.
                            {
                                int büyükIndex;
                                if (duraklar[LeftChild(index)].normalBisiklet >= duraklar[RightChild(index)].normalBisiklet) { büyükIndex = LeftChild(index); }//sol çocuk büyük veya eşit
                                else { büyükIndex = RightChild(index); }//sağ çocuk büyük
                                if (duraklar[index].normalBisiklet < duraklar[büyükIndex].normalBisiklet)
                                {
                                    Durak temp = duraklar[index];
                                    duraklar[index] = duraklar[büyükIndex];
                                    duraklar[büyükIndex] = temp;
                                    index = büyükIndex;//değişim olduğu için döngü en az bir kere daha devam etmelidir.
                                }
                                else//parent daha büyük olduğu için değişim yaplılamaz döngüden çıkılmalı
                                {
                                    break;
                                }

                            }

                            else//sağ çocuk boşsa sadece sol çocuk ile kontrol edilmeli
                            {
                                int büyükIndex = LeftChild(index);
                                if (duraklar[index].normalBisiklet < duraklar[büyükIndex].normalBisiklet)
                                {
                                    Durak temp = duraklar[index];
                                    duraklar[index] = duraklar[büyükIndex];
                                    duraklar[büyükIndex] = temp;
                                    index = büyükIndex;//değişim olduğu için döngü en az bir kere daha devam etmelidir.
                                }
                                else//parent daha büyük olduğu için değişim yaplılamaz döngüden çıkılmalı
                                {
                                    break;
                                }

                            }
                        }
                        else//sol çocuk boşsa sağ çocukta boştur. heapte artık düzenleme yapılamsına gerek yoktur.
                        {

                            break;
                        }
                    }
                    else { break; }
                }
            }
            return döndür;
        }
        public void Add(Durak durak) {
            if (size<maxSize) {//heapde yer varsa ekleme yapılabilir
                int index = size;
                duraklar[index] = durak;//son indexe nesnenin eklenmesi
                size++;
                if (index != 0) {//ilk item ise düzenleme yapılmasına gerek yoktur.
                    while (true)
                    {
                        if (duraklar[index].normalBisiklet > duraklar[Parent(index)].normalBisiklet)
                        {
                            int parentIndex = Parent(index);
                            Durak temp = duraklar[parentIndex];
                            duraklar[parentIndex] = duraklar[index];
                            duraklar[index] = temp;
                            index = parentIndex;
                        }
                        else
                        {
                            break;//büyük değilse düzenleme ihtiyacı bitmiştir.
                        }
                    }
                }
         
            }
        }
        public void PrintMaxHeap()
        {
            for (int i = 0;i<size;i++) {
                Console.WriteLine(duraklar[i].ToString());
            }
            Console.WriteLine("--------------");

        }
    }








    class Program
    {
        static (BinarySearchTree,List<Durak>) for1A(String[] duraklar)
        {

            BinarySearchTree ağaç = new BinarySearchTree();
            List<Durak> durakListesi = new List<Durak>();
            //durakların bilgilerinin alınması
            String[] durağınBilgisi = new String[4];
            
            for (int i=0;i<duraklar.Length;i++) {//ana giriş verisindeki bütün elemanların durak bilgilerinin tek tek gezilmesi
                int count = 0;
                int countForArray = 0; //geçici dizinin elemanlarının gezilebilmesi için.


                for (int y =0;y<duraklar[i].Length;y++)//durağın bilgilerinin oluşturulması
                {
                    if (duraklar[i][y]==',') {//stringde virgülün aranması
                        durağınBilgisi[countForArray]=duraklar[i].Substring(count,(y-count));//virgüle kadarki karakterlerin string olarak diziye eklenmesi
                        
                        count = y+2;//virgülden sonraki boşluğun eklenmemesi için
                        countForArray++;
                    }
                
                }
                durağınBilgisi[countForArray] = duraklar[i].Substring(count,duraklar[i].Length-count);//son karakterlerin geçici diziye eklenmesi

                Durak durak = new Durak(durağınBilgisi[0], int.Parse(durağınBilgisi[1]), int.Parse(durağınBilgisi[2]), int.Parse(durağınBilgisi[3]));//durak nesnesinin oluşturulması

                Durak kopyaDurak = new Durak(durağınBilgisi[0], int.Parse(durağınBilgisi[1]), int.Parse(durağınBilgisi[2]), int.Parse(durağınBilgisi[3]));//ileride ağaçta yapıcağım değişikliklerin hash table etkilememesi için bir kopyasını veriyorum.
                durakListesi.Add(kopyaDurak);
                Random random = new Random();
                
                
                int rastgeleSayı = random.Next(1,11);
                for(int y =0;y< rastgeleSayı; y++)//List tipinde bir veri yapısı içine 1 ile 10 adet arasında random sayıda rastgele Müşterteri eklenmesi
                {
                    Müşteri müşteri = new Müşteri();
                    durak.müşteriler.Add(müşteri);
                }
                
                ağaç.AddToTree( durak);



            }

            return (ağaç,durakListesi);
        }

        static void For1B(BinarySearchTree ağaç) 
        {
            Console.WriteLine("Ağacın derinliği : "+ağaç.DerinlikBul(ağaç.root));
            ağaç.AğaçYaz(ağaç.root);
        }
        static void For1C(BinarySearchTree ağaç)
        {
            Console.WriteLine("Lütfen aranacak Müşteri ID’sini giriniz : ");
            int id = Convert.ToInt32(Console.ReadLine());
            ağaç.SearchFoID(id,ağaç.root);
        }

        static int hashingFuntion(String ad, int elemanSayısı)//harflerin ascii koduna göre toplanması ve  bu toplamın eleman sayısına göre modunun alınmasıyla anahtar oluşturulması
        {
            int toplam = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(ad);
            for (int i = 0; i < asciiBytes.Length; i++)
            {
                toplam += asciiBytes[i];
            }
            return toplam -(toplam/elemanSayısı)*elemanSayısı;//kalanın bulunması
        }

        static List<Durak>[]  For2A(List<Durak> durakListesi)
                                                                // kelimelerin ascii kod değerlerinin eleman sayısına göre modunun alınıp index yaratılıyor.
                                                                   // aynı index birden fazla kelime için üretilebilir bu nedenle hata oluşumunu engelllemek için
                                                                   //ayrı zincirleme yöntemini tercih ettim.
        {
            int durakSayısı = durakListesi.Count;
            List<Durak>[] duraklar = new List<Durak>[durakSayısı];//durak sayısına göre , eleamanları durakları içeren liste olan bir listenin oluşturulması.
            
            foreach(Durak durak in durakListesi)
            {
                int index =hashingFuntion(durak.durakAdı, durakSayısı);
                if (duraklar[index]== null) //key e göre hash table a ekleme
                {
                    List<Durak> eklenecekDurak = new List<Durak>();
                    eklenecekDurak.Add(durak);
                    duraklar[index] = eklenecekDurak;
                }
                else
                {
                    duraklar[index].Add(durak);
                }
            }
            return duraklar;
        
        }
        static void For2B(List<Durak>[] durakListesi)
        {
            for (int i = 0; i < durakListesi.Length; i++)
            {
                if (durakListesi[i]!=null)//ayrı zincirleme hash tableda bazı indexler doldurlmamış olabilir
                {
                    foreach (Durak durak in durakListesi[i]) {
                        if (durak.boşPark>5) {
                            durak.boşPark -= 5;
                            durak.normalBisiklet += 5;
                        }
                    }
                }
            }

        }
        static void printHashed(List<Durak>[] hashedDuraklar)
        {

            for (int i = 0; i < hashedDuraklar.Length; i++)
            {
                Console.Write(i + ": ");
                if (hashedDuraklar[i] != null)
                {
                        
                    foreach (Durak dur in hashedDuraklar[i])
                    {
                        Console.Write(dur.ToString()+",");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------");
            Console.WriteLine("----------");

        }
        static MaxHeap For3B(List<Durak> durakListesi)
        {
            MaxHeap heap = new MaxHeap(durakListesi.Count);
            for (int i = 0; i < durakListesi.Count; i++)
            {
                heap.Add(durakListesi[i]);
            }
            return heap;
        }
        static void For3C(MaxHeap heap)
        {
            for (int i =0;i<3;i++)
            {
                Console.WriteLine(heap.getMax().ToString());
            }
        }

        static void For4A(List<Durak> duraklistesi)//insertion sort. normal bisiklet sayıları için sıralama yapılmıştır.büyükten küçüğe sıralıyor.
        {
            Durak temp = null;
                        //başlangıçta ilk itemin sıralanmasına gerek yoktur.
            for (int sortedLast=1 ; sortedLast<duraklistesi.Count;sortedLast++)//listedeki bütün itemların dönülmesi için
            {
                temp = duraklistesi[sortedLast];//bellekte gecici olarak işlem yapılan nesnenin tutulması
                int index = sortedLast;//değişim işlemlerine başlanılıcak index
                while (index >= 1 && temp.normalBisiklet > duraklistesi[index-1].normalBisiklet)//başlangıç noktasından e
                {
                    duraklistesi[index ] = duraklistesi[index-1];
                    index--;
                }
                duraklistesi[index] = temp;
            }
            foreach (Durak dur in duraklistesi) { Console.WriteLine(dur.ToString()); }
        }


        static void For4B(List<Durak> duraklistesi)//quick sort. Normal bisiklet sayıları için sıralama yapılmıştır.büyükten küçüğe doğru sıralıyor.
        {   
            //diğer fonksiyonlar listenin girildiği fonksiyonun scopu içerisinde oldukları için listenin diğer fonksiyonlara parametre olarak girilmesi gerek kalmıyor
            quickSort(0,duraklistesi.Count-1);//programın çalıştırılması    
            foreach (Durak dur in duraklistesi) { Console.WriteLine(dur.ToString()); } // sıralama sonrasında listenin yazdırılma kontrolü


            void quickSort(int baş, int son)
            {
                if (baş < son)
                {

                    int pivot = partition(baş, son);//pivot olarak kullanılan nesne listede olması gereken yerde

                    quickSort(baş, pivot - 1);  // pivottan önceki gurupun sıralanması
                    quickSort(pivot + 1, son); // pivottan sonraki gurupun sıralanması
                }
            }

            int partition(int baş, int son)
            {
                
                Durak temp=null;//bellekte gecici olarak işlem yapılan nesnenin tutulması için


                int pivot = duraklistesi[son].normalBisiklet;// pivot değernin atanması

                int i = (baş - 1);  // en baştaki itemin indexi

                for (int j = baş; j <= son - 1; j++)
                {
                    
                    if (duraklistesi[j].normalBisiklet > pivot)//şuanki nesnenin değeri pivottan büyükse değişim yapılmalı
                    {
                        i++;    // en küçük itemin indexinin arttırılması

                        //nesnelerin indexlerinin değiştirilmesi.
                        temp = duraklistesi[i];
                        duraklistesi[i] = duraklistesi[j];
                        duraklistesi[j] = temp;
                        
                    }
                }
                //sonuncu nesnenin pivotun yerine getirilmesideğiştirilmesi.
                temp = duraklistesi[i+1];
                duraklistesi[i+1] = duraklistesi[son];
                duraklistesi[son] = temp;

                return (i + 1);//pivotun döndürülmesi
            }


        }


        static void Main(string[] args)

        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            String[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Konak, 7, 0, 5", "Çankaya, 13, 0, 2", "Basmane, 55, 2, 12", "Hilal, 6, 4, 11", "Halkapınar, 23, 4, 0", "Stadyum, 17, 1, 11", "Bornova Hastahane, 5, 3, 15" };


            BinarySearchTree ağaç;
            List<Durak> durakListesi;
            
            //1-
            (ağaç,durakListesi)=for1A(duraklar);
            Console.WriteLine("");
            For1B(ağaç);
            Console.WriteLine("");
            For1C(ağaç);

            //For1D
            Console.WriteLine("");
            ağaç.Kiralama("İnciraltı",20);

            //2-
            Console.WriteLine("");
            List<Durak>[]  hashedDuraklar = For2A(durakListesi);
            printHashed(hashedDuraklar);
            For2B(hashedDuraklar);
            printHashed(hashedDuraklar);

            //3-
            Console.WriteLine("");
            MaxHeap heap=For3B(durakListesi);
            heap.PrintMaxHeap();
            For3C(heap);

            //4-
            Console.WriteLine("for4");
            Console.WriteLine("-------");
            For4A(durakListesi);
            For4B(durakListesi);
        }
    }
}
