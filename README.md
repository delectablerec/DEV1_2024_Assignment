## Progettazione webapp bracciali

#### Questo README ha lo scopo di progettare l'impostazione generale dell'applicazione web dedicata al catalogo della gestione dei prodotti.

### Funzionalità della webapp

L'applicazione, per la sua concezione iniziale, ha lo scopo di mettere ad disposizione una piattaforma che permette agli utenti di acquistare dei gioielli messi a disposizione da indie-brand che hanno la possibilità di registrarsi, ed in seguito ad approvazione da parte di un amministratore, di caricare i propri articoli su questa piattaforma. 
Per il momento l'applicazione permette di mettere in vendita unicamente bracciali, ma si vuole espandere la vendita anche ad orecchini, collante e anelli. 

### Compiti da organizzare

- [x] Progettare struttura pagine web
- [x] Occuparsi della gestione dei ruoli

### Requisiti della webapp

- [x] Responsività del sito (bootstrap)
- [ ] Partial Views per parti di codice che vengono ripetute
- [ ] ViewModels Specifici per ogni pagina
    - [x] IndexViewModel
    - [x] ProductDetailsViewModel
    - [x] ShoppingCartViewModel
    - [x] AdminViewModel
    - [x] BrandManagementViewModel
- [x] Decidere tutti gli elementi relativi a foglio di stile css
- [x] Proprietà bool per prodotto per controllare che sia valido
- [ ] Filtrare per Brand tramite le immagini
- [ ] Subscription based website
 
## Struttura del sito

## Pagine

- Layout
- [x] Navbar responsiva, con a vista continua il botton per accedere al carrello e completare la vendita
- [x] Footer con le generalità della società

- Homepage
- [x] carousel dei prodotti piu venduti
- [x] card di prodotti più venduti, di prodotti più convenienti, totale vendite effettuate, totale clienti
- [x] carousel con i loghi dei brand
- [x] bottone che rimanda al Catalogo prodotti

- Catalogo Prodotti:
- [x] lista dei prodotti disponibili messi su cards, dove si visualizza nome, prezzo, bottone per aggiungere al carrello e bottone per i dettagli
- [x] filtri per prezzo, per materiale, per brand
- [x] ricerca per nome
- [ ] ordina per prezzo

- Gestione Prodotti admin:
    - Bottone Aggiungi
    - Bottone Modifica
    - Bottone Elimina

    - Accetta il produttore
    - Accetta prodotto del produttore

- Gestione Prodotti brand:
    - Bottone Aggiungi
    - Bottone Modifica
    - Bottone Elimina
    
## Passi 

### 1. Fasi iniziali
- [x] Inizializzazione dell'archetipo della Webapp

- [x] Predisposizione e migrazione del database
    - [x] SeedData: compilazione del database con l'inizializzazione dei ruoli Admin, Customer, Brand

    Per poter accedere come amministratore:
    ```Email = "admin@admin.com"```
    ```Password = "Password1@"```
    
    - [x] Aggiunta dei modelli Brand e Products e collegamento con EF (assicurandosi che ApplicationDbContext erediti da IdentityDbContext)

### 2. Funzionamento della pagina Catalogo Prodotti

Questa pagina consente di visualizzare l'intero inventario presente in database. 

- [x] visualizzazione del solo elenco dei prodotti
- [x] visualizzazione del numero di pagine
- [x] visualizzazione dei bottoni AddToCart + Details e collegamento alle relative pagine
- [x] visualizzazione dei filtri per prezzo e relativo funzionamento
- [x] visualizzazione della barra di ricerca per nome e relativo funzionamento
- [x] possibilità di filtrare per Brand 
- [x] possibilità di ordinare per prezzo

### 3. Funzionamento della pagina Cart

Questa pagina consente di visualizzare il carrello del singolo utente. 

- [x] Creazione del ViewModel CartViewModel.cs

- [x] Predisposizione della pagina html:
    - [x] visualizzazione dei prodotti aggiunti al carrello
    - [x] bottone per aggiornare il carrello
    - [x]bottone per eliminare l'articolo
    - [x]collegamento all'acquisto, che scala lo stock degli articoli acquistati dal database

- [x] Rendere operativo: 
    - [x] metodo Cart che gestisce la visualizzazione del carrello del singolo utente. Per farlo, nel `ProductService.cs ` occorre aggiungere un metodo per la lettura del carrello dal file JSON
    - [x] metodo AddToCart per l'aggiunta al carrello da Product/Index
    - [x] metodo UpdateCartQuantity per modificare la quantità in fase di acquisto 
    - [x] metodo RemoveFromCart per eliminare l'articolo in fase di acquisto 
    - [x] metodo Purchase che finalizza l'acquisto, che scala lo stock degli articoli acquistati dal database e che non permette l'acquisto in caso lo stock sia insufficiente
    - [x] mostrare totale del carrello, del totale per prodotto 

### 4. Scaffolding delle pagine 
    - [x] per Register e Login: aggiunta campo Username, di default Identity gli assegna l'email, noi lo assegnamo al nome del brand
    - [x] possibilità di registrarsi come brand per vendere i propri prodotti 
    - [x] impostazione nella pagina di Login del campo User invece che del campo Mail 

### 5. Pagina di gestione del proprio archivio (Brand) 
- [x] Predisposizione della pagina html - la pagina contiene:
    - [x] bottone AddProduct per aggiungere il proprio prodotto
    - [x] barra di ricerca per cercare un prodotto per nome
    - [x] una galleria dei propri prodotti inseriti, con all'interno della card un bottone Edit e Delete
- [x] Rendere operativo: 
    - [x] all'aggiunta del prodotto che venga rimandato all'approvazione dell'admin
    - [x] visualizzazione di prodotti del solo brand
    - [x] metodo EditProduct: possibilità quindi di modificarli
    - [x] metodo DeleteProduct: possibilità quindi di eliminarli

### 5. Pagina di gestione dell'Admin (Brand) 
- [x] Predisposizione della pagina html - la pagina contiene una lista di prodotti da mandare in approvazione
- [x] Rendere operativo: 
    - [x] metodo AcceptProduct: possibilità quindi di accettarli
    - [x] metodo DeleteProduct: possibilità quindi di eliminarli