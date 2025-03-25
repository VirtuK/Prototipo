# Protótipo de Match-3 (Jogo estilo Candy-Crush)

Um protótipo feito tentando implementar da melhor maneira as mecânicas básicas de um jogo Match-3.

## Índice

- Introdução
- Mecânicas e Features
- Estrutura do Projeto
- Documentação do Código
  
## Introdução

Este projeto implementa as principais funcionalidades de um jogo Match-3 utilizando a Unity 2022.3.60f1. Ele abrange desde a geração procedural do tabuleiro até as mecânicas de seleção e verificação de combinações, incluindo o sistema de dicas, pontuação, movimentos restantes, menus e a criação de fases personalizadas.

Todos os assets de arte utilizados no projeto pertencem ao pacote de asset: [**Hungry Bat - Match 3 UI [FREE]**](https://assetstore.unity.com/packages/2d/gui/hungry-bat-match-3-ui-free-229197). Eles foram escolhidos para melhorar a aparência visual do protótipo, permitindo que o foco principal fosse no desenvolvimento da programação.

## Mecânicas e Features

### Menus

#### Menu Principal
Logo ao abrir o protótipo, a primeira coisa que o jogador verá é o menu principal, contendo as opções para entrar em três fases já pré-determinadas, criar uma fase personalizada ou sair do jogo.

![image](https://github.com/user-attachments/assets/49e5fa4d-47ff-4258-bbe7-a77373cabfa1)

Selecionar qualquer uma das três primeiras opções inicia diretamente uma fase. O botão "Custom" abre o menu de fase personalizada, como mostrado abaixo.

### **Menu de Fase Personalizada**  

Este menu permite ao jogador definir a configuração da fase, ajustando o número de linhas e colunas, a pontuação necessária para vencer e o número de movimentos disponíveis. Essas configurações são feitas por meio de inputs numéricos nas caixas destacadas.  

- **Botão "Start"** → Inicia a fase com as configurações selecionadas.  
- **Botão "Return"** → Retorna ao menu principal.  

![Menu de Fase Personalizada](https://github.com/user-attachments/assets/52625bff-765e-4c60-ad20-2680910d1b93)  

---

### **Telas de Vitória e Derrota**  

Durante a gameplay, o jogador pode vencer ao atingir a pontuação alvo dentro do limite de movimentos ou perder caso os movimentos se esgotem antes de alcançar a meta.  

Ao final de uma fase, uma dessas duas telas aparecerá, exibindo:  
- **Uma mensagem informando o resultado** (vitória ou derrota).  
- **A pontuação final do jogador**.  
- **Um botão para retornar ao menu principal**.  

As imagens abaixo mostram, respectivamente, a **Tela de Vitória** e a **Tela de Derrota**.  

![Tela de Vitória](https://github.com/user-attachments/assets/01892bb6-123c-428e-8ff5-4dea07590cca)  
![Tela de Derrota](https://github.com/user-attachments/assets/68e42db8-c447-4abd-833b-f5e5c13eca0f)  

---

### **O Tabuleiro e o HUD**  

Ao iniciar qualquer fase, a seguinte tela será exibida:  

![Tela de Jogo](https://github.com/user-attachments/assets/1d1ea0f7-6db9-4c07-97f4-b20def12a257)  

Essa é a **tela principal do jogo**, que contém:  
- **A pontuação atual do jogador** e a **pontuação necessária para vencer**.  
- **O número de movimentos restantes**.  
- **O tabuleiro de jogo**, onde ocorrem as combinações.  

O tamanho do tabuleiro pode ser totalmente personalizado. Abaixo estão alguns exemplos:  

**Tabuleiro 7x7:**  
![Tabuleiro 7x7](https://github.com/user-attachments/assets/bda77ab2-fc30-4085-8ce5-dcc681f5a0cb)  

**Tabuleiro 10x10:**  
![Tabuleiro 10x10](https://github.com/user-attachments/assets/8a9b46e3-868e-4d0e-a238-d51d2cead73a)  

O sistema permite gerar tabuleiros de qualquer tamanho, incluindo configurações assimétricas, como **3x5**, por exemplo.  

---

### **As Peças**  

O tabuleiro é composto por diversas peças como esta:  

![Peça do Jogo](https://github.com/user-attachments/assets/eec0f7ae-2fa5-494f-9336-0074060e701c)  

Essas são as peças que devem ser combinadas para ganhar pontos. Neste protótipo, existem **7 variações de peças**, cada uma com um ícone distinto e um valor de pontuação específico. _(É possivel gerar infinitas peças diferentes a partir do script Item.cs)_.  

Quando o jogador seleciona uma peça, seu fundo muda de cor para indicar a seleção:  

![Peça Selecionada](https://github.com/user-attachments/assets/4063a2dd-0166-4366-b838-86ecd375ad93)  

Após selecionar uma peça, se o jogador tocar em outra adjacente, o jogo tentará trocá-las de lugar:  
- **Se a troca resultar em uma combinação válida**, as peças mudam de posição, a combinação é destruída, e o jogador ganha pontos.  
- **Se não for possível formar uma combinação**, as peças permanecem no lugar.  

Aqui está um exemplo de combinação sendo formada:  

![Combinação de Peças](https://github.com/user-attachments/assets/bd478183-3a52-4972-920c-8868b2f39d7f)  

O protótipo também possui um **sistema de dicas**, que destaca uma possível jogada caso o jogador fique muito tempo sem fazer um movimento.  

![Sistema de Dicas](https://github.com/user-attachments/assets/17784bdd-6045-48b2-92fd-e8e48be60431)  

---

## Estrutura do Projeto

### Pastas

Prototipo </br>
├── 📁 Assets </br>
│   ├── 📁 Animations #Os arquivos de animação do projeto, que é apenas uma. </br>
│   │   ├── Point_Texts📁 #A animação que aparece quando o jogador pontua   </br>
│   ├── 📁 Items #Contém todos os possiveis itens que as peças podem conter </br>
│   ├── 📁 Prefabs #Contém os prefabs das linhas, das peças e do texto de pontuação </br>
│   ├── 📁 Scenes #Contém as duas cenas do jogo: Menu e a Cena do Jogo </br>
│   ├── 📁 Scripts  # Todos os scripts do jogo </br>
│   │   ├── Match-3 📁 #Os scripts que influenciam o jogo </br>
│   │   │   ├── Managers 📁 #Os scripts que gerenciam elementos do jogo (tabuleiro, troca de peças, etc.)  </br>
│   │   │   │   ├── AnimationController.cs #Realiza a animação de troca de peças e quando uma peça nova surge  </br>
│   │   │   │   ├── BoardManager.cs #Realiza toda a criação de tabuleiro e gerencia os movimentos  </br>
│   │   │   │   ├── GameUtilities.cs #Contém algumas funções que são usadas por vários scripts  </br>
│   │   │   │   ├── HintManager.cs #Gerencia todo o sistema de dicas ativando e desativando quando necessario </br>
│   │   │   │   ├── TileManager.cs #Gerencia cada peça do jogo, verificando combinações, e ajeitando as peças </br>
│   │   │   │   ├── UIManager.cs #Cuida dos elementos da UI do jogo  </br>
│   │   │   ├── Item.cs #Scriptable Object que guarda as informações de cada item  </br>
│   │   │   ├── Row.cs #Utilizado por cada linha do tabuleiro para guardar informações das peças delas </br>
│   │   │   ├── Tile.cs #Utilizado por cada peça do tabuleiro para guardar informações do item dela e gerenciar o icone na tela  </br>
│   │   ├── Menu📁 #Os scripts que gerenciam os menus </br>
│   │   │   ├── Game 📁 #Os scripts que gerenciam os menus que aparecem na cena do jogo  </br>
│   │   │   │   ├── GameUiButtons.cs #Guarda as informações das telas de vitoria e derrota e os botões delas </br>
│   │   │   ├── MainMenu 📁 #Os scripts que gerenciam os menus que aparecem no menu principal  </br>
│   │   │   │   ├── Buttons.cs #Guarda as informações das telas dos menu e os botões delas  </br>
│   │   ├── LevelManager.cs #Script que instancia e guarda as informações de fase que são pega no menu para criar o tabuleiro </br>
│   ├── 📁 Simasarts #Os assets do pacote de arte que estou utilizando </br> 
│   ├── 📁 TextMesh Pro #Os assets TextMesh Pro padrão da Unity </br>
└──               

---

### Cenas do Projeto

O projeto conta com duas cenas na Unity: SampleScene (Cena padrão que foi utilizada como cena de jogo) e Menu (Cena do Menu Principal) </br>
Elas estão organizado da seguinte forma:

#### Cena de Jogo

![image](https://github.com/user-attachments/assets/796a91ed-c986-40dc-b3b1-7af0008d5e14)

Os objetos a se destacar são:
- Board: O obejto que contem o BoardManager.cs, é aonde todas as linhas e peças são geradas.
- Scores_Text e Moves_Text: Os textos da HUD.
- VictoryScreen e GameOverScreen: Os objetos das telas de vitória e derrota, que ficam desativados até serem chamados pelos scripts.
- Game_Manager: Objeto vazio que guarda os scripts de AnimationController.cs, TileManager.cs e HintManager.cs.
- UI_Manager: Objeto vazio que guarda os scripts de UIManager.cs e GameUiButtons.cs.

#### Cena de Menu

![image](https://github.com/user-attachments/assets/5436e47c-2a90-4f90-a29a-58721673e2cd)

Os objetos a se destacar são:
- Buttons: Guarda tudo do Menu Principal.
- Custom: Guarda tudo do Menu de Fase Personalizada.
- LevelManager: Objeto vazio que guarda o script LevelManager.cs.

---

## Documentação do Código

Nesse trecho, comentarei cada script do projeto e qual o próposito de cada função deles.

### AnimationController.cs

- SwapItems() #Coroutine que realiza a troca de itens entre duas peças com uma animação suave.
- AnimateNewTile() #Coroutine que anima a aparição de uma nova peça escalando-a do zero até seu tamanho original.

### BoardManager.cs

- Start() #Inicializa as configurações do tabuleiro e gera o tabuleiro do jogo
- GenerateBoard() #Cria o tabuleiro gerando as linhas e peças, atribuindo itens aleatórios a cada peça
- ResetBoard() #Reinicia o tabuleiro destruindo os tiles atuais e gerando um novo tabuleiro
- Update() #Atualiza a interface do usuário com a pontuação e o número de movimentos restantes
- DecrementMoves() #Decrementa o número de movimentos e verifica se o jogador venceu ou perdeu
- AddScore() #Adiciona pontos ao placar
- GetRows() #Retorna a lista de linhas do tabuleiro
- GetItems #Retorna a lista de itens disponíveis no jogo

### GameUtilities.cs

- AreAdjacent() #Verifica se duas peças são adjacentes horizontal ou verticalmente
- SwapTiles() #Troca os itens e ícones entre duas peças

### HintManager.cs

- Update() #Verifica se o tempo para uma nova dica foi alcançado
- ResetTimer() #Reinicia o temporizador de dica e limpa as dicas exibidas
- ShowHint() #Encontra um movimento válido e destaca as peças relacionadas
- HighlightTiles() #Destaca as duas peças que formam o movimento válido encontrado
- ClearHints() #Limpa as animações de dica em todas as peças

### TileManager.cs

- SelectTile() #Seleciona uma peça e ativa a verificação caso já tiver uma peça selecionada
- SwapAndCheck() #Troca as peças e verifica se há combinações
- CheckForMatches(com paramêtros) #Verifica se há combinações no tabuleiro
- CheckForMatches(sem paramêtros) #Retorna o resultado da verificação
- HandleMatches() #Lida com as combinações, limpando e reorganizando o tabuleiro
- ClearMatches() #Limpa as peças combinadas, cria o texto de pontos delas e adiciona os pontos no score
- ShiftTilesDown() #Move as peças que possuem espaços vazios abaixo delas para baixo
- RefillBoard() #Preenche o tabuleiro com novas peças
- WaitAndCheck() #Aguarda e verifica se há movimentos possíveis, também limpa os textos de pontos da tela
- HasPossibleMoves(com paramêtros) #Verifica se há movimentos possíveis no tabuleiro
- HasPossibleMoves(sem paramêtros) #Retorna o resultado da verificação
- RegenerateNewTiles() #Recria as novas peças do tabuleiro e garante que sempre tenha uma combinação possivel pelo menos
- FindValidMoves() #Verifica se existe algum movimento válido no jogo

### UIManager.cs
- UpdateScoreText() #Atualiza o texto de Score da UI
- UpdateMovesText() #Atualiza o texto de movimentos da UI

### Item.cs
Não possui funções apenas guarda as informações dos Scriptable Objects.

### Row.cs
- CreateTile() #Cria uma peça e adiciona à lista de peças da linha

### Tile.cs
- Start() #Inicializa referências da cena
- Select() #Coloca essa peça como selecionada
- StartHintAnimation() #Inicia a animação de dica
- ClearHintAnimation() #Limpa a animação de dica, retornando a cor original
- HintAnimation() #Animação de dica que alterna entre duas cores (normal e destaque)
- HighlightSelect() #Destaca a peça selecionada
- ResetHighlight() #Restaura a cor original

### GameUiButtons.cs
- VictoryScreen() #Exibe a tela de vitória e atualiza a pontuação final
- GameOverScreen() #Exibe a tela de game over e atualiza a pontuação final
- UpdateScoreText() #Atualiza o texto de pontuação exibido na tela de vitória ou game over
- ReturnToMenu() #Retorna o jogador ao menu principal

### Buttons.cs
- StartLevel() #Inicia o nível, definindo as configurações e carregando a cena principal
- OpenCustom() #Abre o menu de personalização de nível
- CloseCustom() #Fecha o menu de personalização e retorna ao menu principal
- ChangeRows() #Atualiza o número de linhas com o valor inserido pelo jogador
- ChangeColumns() #Atualiza o número de colunas com o valor inserido pelo jogador
- ChangeMoves() #Atualiza o número máximo de movimentos com o valor inserido pelo jogador
- ChangeScore() #Atualiza a pontuação personalizada com o valor inserido pelo jogador
- CloseGame() #Fecha o jogo

---

