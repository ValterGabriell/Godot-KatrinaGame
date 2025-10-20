

# Documento Detalhado de Mecânicas – Myha Escapa da Prisão

## 1. Mecânicas de Movimento

- **Idle:** Myha parada, postura baixa, pronta para reagir.
- **Andar Furtivo (Stealth Walk):** Passos lentos, quase sem ruído, ideal para atravessar sombras e áreas próximas a inimigos.
- **Correr:** Movimento rápido, gera ruído alto, útil para fugir, mas arriscado.
- **Pular:** Permite acessar prateleiras, vigas, dutos e rotas verticais.
- **Interagir:** Usar esconderijos, terminais, itens do cenário.

## 2. Sistema de Luz e Sombra

- **Sombras:** Myha fica invisível para inimigos (exceto em alerta máximo). Feedback: outline preto + olhos verdes.
- **Luz:** Myha é facilmente detectada. Feedback: corpo iluminado, destaque amarelo.
- **Fontes de luz:** Fixas ou controláveis via terminais. Algumas podem ser desligadas temporariamente.

## 3. Esconderijos Temporários

- **Tipos:** Armários, caixas, dutos, sob mesas.
- **Funcionamento:** Myha entra/sai rapidamente. Inimigos não a detectam enquanto escondida.
- **Tensão:** Timer de suspense quando inimigo passa perto. Se o inimigo investigar, Myha pode ser pega.

## 4. Distração por Objetos/Som

- **Itens lançáveis:** Bola de pelo, bandeja, pedra, panela.
- **Interação ambiental:** Derrubar objetos, ativar alarmes, bater em grades.
- **Feedback:** Círculo visual mostra alcance do som. Inimigos investigam por tempo limitado.
- **Limite:** Quantidade de objetos por área, cooldown entre usos.

## 5. Rotas Verticais e Dutos

- **Prateleiras/vigas:** Permitem evitar patrulhas terrestres.
- **Dutos:** Conectam áreas, são zonas seguras, mas podem ter trechos ruidosos.
- **Acesso:** Pulo ou interação direta.

## 6. Sistema de Terminais

- **Funções:** Checkpoint, comunicação com Katrina/Didi, desligar luzes, criar distrações remotas, revelar rotas seguras.
- **Interface:** Tela verde ácido, feedback sonoro e visual ao ativar.
- **Limite:** Só podem ser ativados em pontos específicos do mapa.

## 7. Detecção por Som

- **Ações que geram som:** Andar, correr, pular, lançar objeto, interagir com itens.
- **Feedback:** Círculos coloridos (verde = silencioso, amarelo = audível, vermelho = alto).
- **IA:** Inimigos mostram ícone de ouvido ao detectar som, mudam patrulha para investigar.

## 8. Estados de Alerta dos Inimigos

- **Unaware (Branco):** Patrulha normal, não percebe Myha.
- **Suspicious (Amarelo):** Ouviu som ou viu algo suspeito, investiga área.
- **Alert (Laranja):** Viu Myha brevemente, procura ativamente, chama reforços.
- **Chase (Vermelho):** Viu Myha claramente, persegue até perder de vista, outros guardas ficam alertas.

## 9. Checkpoints e Progressão

- **Checkpoints:** Apenas em terminais ativados.
- **Falha:** Ao ser detectada, Myha volta ao último terminal.
- **Progressão:** Linear, cada fase introduz novas mecânicas ou combinações.

## 10. Feedback Visual e Sonoro

- **Cones de visão:** Sempre visíveis, mudam de cor conforme estado do inimigo.
- **Círculos de som:** Mostram alcance do ruído.
- **Música:** Dinâmica, aumenta tensão conforme risco.
- **Partículas:** Olhos dos inimigos brilham, partículas laranja em alerta.

***

## 11. Comportamento dos Inimigos

### Gato Guarda Básico
- **Patrulha:** Linha reta ou círculo, velocidade média.
- **Alcance visual:** Médio (5 tiles), cone de visão padrão.
- **Alcance auditivo:** Médio (4 tiles).
- **Reação:** Investiga sons, persegue ao ver Myha.
- **Diferenciação:** Pelagem branca, olhos laranja.

### Gato Patrulheiro
- **Patrulha:** Múltiplos waypoints, para para observar.
- **Alcance visual:** Médio (5 tiles), cone de visão mais largo.
- **Alcance auditivo:** Alto (5 tiles).
- **Reação:** Mais atento a distrações, retorna à patrulha após investigar.
- **Diferenciação:** Uniforme roxo, boina, olhos laranja.

### Gato Vigia (Estático)
- **Patrulha:** Fixo, rotaciona 180-270°, maior alcance visual (7 tiles).
- **Alcance auditivo:** Baixo (3 tiles).
- **Reação:** Só reage a Myha na linha de visão.
- **Diferenciação:** Pelagem cinza claro, visor branco, olhos grandes.

### Gato Líder/Boss
- **Patrulha:** Imprevisível, velocidade alta, muda padrões.
- **Alcance visual:** Alto (8 tiles).
- **Alcance auditivo:** Muito alto (6 tiles).
- **Reação:** Persegue agressivamente, ativa alarmes.
- **Diferenciação:** Pelagem roxa, uniforme laranja, olhos amarelos.

### Câmera de Segurança
- **Patrulha:** Rotação 360° lenta.
- **Alcance visual:** Muito alto (10 tiles), sem audição.
- **Reação:** Ativa alarme ao detectar Myha.
- **Diferenciação:** Corpo cinza metálico, lente laranja piscante.

### Drone Voador (Opcional)
- **Patrulha:** Vertical, cobre rotas elevadas.
- **Alcance visual:** Médio (6 tiles), sem audição.
- **Reação:** Alerta guardas ao detectar Myha.
- **Diferenciação:** Corpo cinza, luz laranja.

***

## 12. Itens e Interações

- **Bola de pelo:** Distração, pode ser lançada.
- **Bandeja metálica:** Distração, faz barulho ao cair.
- **Chave:** Abre portas específicas.
- **Painel de controle:** Ativa/desativa luzes, portas, alarmes.
- **Terminal:** Checkpoint, comunicação, ativa eventos.
- **Ferramenta:** Pode abrir dutos ou criar distração.
- **Arbusto/pedra (pátio):** Esconderijo ou distração natural.
- **Alavanca:** Usada em puzzles ou para abrir rotas alternativas.

***

## 13. Outras Mecânicas de Suporte

- **Tutorial dinâmico:** Primeiras fases ensinam cada mecânica de forma orgânica.
- **Progressão de dificuldade:** Novos inimigos, armadilhas e combinações a cada fase.
- **Sistema de falha justa:** Sempre há múltiplas soluções para cada desafio.
- **Variedade de rotas:** Sempre pelo menos dois caminhos possíveis por área.

***
