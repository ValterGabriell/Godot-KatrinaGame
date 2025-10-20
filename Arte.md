# Guia de Paleta de Cores e Design Visual – Projeto Stealth

Documentação de design da paleta de cores para garantir consistência visual e clareza de gameplay (Stealth). A paleta é limitada a 8 cores para máximo contraste e comunicação visual eficiente.

---

## 1. Paleta de Cores Base (8 Cores Fixas)

A paleta foi desenhada para garantir máximo contraste entre personagens e ambientes, essencial para a clareza visual e o gameplay de stealth.

| Nome da Cor | Código HEX | Aplicação Primária |
| :--- | :--- | :--- |
| Preto Profundo | #1A1A1A | Sombras, Outlines, Áreas Ocultas, Dutos |
| Cinza Escuro (Sombra) | #3D3D3D | Paredes, Chão, Stealth Zones Seguras |
| Cinza Claro (Metal) | #7A7A7A | Grades, Portas, Itens Metálicos |
| Amarelo Prisão | #E5D68A | Luzes, Áreas de Risco, Highlights em Itens (Luz) |
| Verde Ácido (Detalhe) | #6BAA75 | Terminais, Telas, Olhos de Myha |
| Laranja Alerta | #E85D3C | Alarmes, Estados de Alerta, Botões Perigosos |
| Branco Sujo | #E8E8D0 | Pelagem de Inimigos, Reflexos, Papel, Comida |
| Roxo Escuro (Opcional) | #4A3A5C | Uniformes de Patrulheiros, Detalhes High-Tech |

---

## 2. Personagens & Hierarquia Visual

### 2.1. Protagonistas - Família Verde/Cinza

| Personagem | Cores Principais | Identificação Instantânea |
| :--- | :--- | :--- |
| Myha | Cinza Escuro + Olhos Verde Ácido | O verde é único e essencial para o estado de "stealth" (apenas os olhos brilham). Silhueta: orelhas triangulares. |
| Didi | Cinza Claro + Olhos Amarelo Prisão | Inversão de paleta de Myha (cores mais claras). Silhueta: orelhas arredondadas. |

### 2.2. Inimigos - Hierarquia por Cores

A complexidade e a cor indicam a hierarquia de ameaça.

| Inimigo | Cores Principais | Nível de Ameaça Visual |
| :--- | :--- | :--- |
| Gato Básico | Branco Sujo + Olhos Laranja Alerta | Comum e menos ameaçador. |
| Patrulheiro | Branco Sujo + Uniforme Roxo Escuro | Unidade de Elite Visual (o roxo indica maior patente). |
| Vigia | Cinza Claro + Equipamentos Branco Sujo | Torre Tecnológica. |
| Líder/Boss | Roxo Escuro Dominante + Laranja Alerta | Único, intimidante, com a paleta mais complexa. |

---

## 3. Princípios de Design Aplicados

- Silhuetas Distintivas: Formato único para cada personagem (Ex: Myha – orelhas pontudas; Inimigos – formatos militares).
- Comunicação de Estado:
    - Myha nas Sombras: Apenas outline sutil + Olhos Verde Ácido brilhando.
    - Inimigos Alertas: Olhos piscando (Laranja Alerta), partículas de cor.
- Iluminação como Gameplay: O sistema Luz/Sombra é comunicado pela paleta.
    - Áreas Amarelo Prisão = Perigo / Risco.
    - Áreas Cinza Escuro/Preto Profundo = Segurança / Stealth Zone.

---

## 4. Planejamento das Fases (Progressão e Atmosfera)

Cada uma das 7 fases adapta a paleta de 8 cores para transmitir atmosfera e progressão.

**Regra:** Não usar mais de 4 cores simultâneas em um único sprite/item para manter a clareza.

| Fase | Atmosfera e Cores Dominantes | Destaques de Gameplay | Itens Essenciais (Cor) |
| :--- | :--- | :--- | :--- |
| Tutorial/Prologue | Preto, Cinza Escuro (Opressora) | Luz Amarelo Prisão apenas em áreas de perigo. | Chave (Cinza Claro), Terminal (Verde Ácido) |
| Bloco de Celas | Cinza, Amarelo, Branco | Uniformes inimigos Branco Sujo se destacam. | Armário (Cinza Claro), Crachá (Laranja Alerta) |
| Cozinha/Refeitório | Cinza Claro, Branco Sujo | Itens de distração brilham (Amarelo/Verde). | Bandeja (Cinza Claro), Comida (Branco Sujo) |
| Área de Manutenção | Cinza Escuro, Roxo Escuro | Introdução do Roxo Escuro nos Patrulheiros. Dutos Preto Profundo criam rotas seguras. | Ferramenta (Cinza Claro), Duto (Preto Profundo) |
| Corredor de Segurança | Laranja Alerta, Amarelo Prisão | Luzes piscando, cones de visão laranja, Myha precisa usar sombras. | Câmera (Cinza Claro + Laranja), Botão de Alarme (Laranja) |
| Pátio Externo | Cinza Claro, Verde Ácido | Arbustos verde ácido, bancos cinza claro, portão preto profundo, luz natural amarela, áreas de sombra. | Arbusto (Verde Ácido), Banco (Cinza Claro), Pedra (Preto Profundo) |
| Sala de Controle/Fuga Final | Roxo Escuro, Laranja Alerta, Verde | Boss com paleta única. Laranja Alerta indica perigo máximo. | Painel (Verde Ácido), Alavanca (Laranja Alerta) |

---

**Dica:** Use este guia para garantir consistência visual, clareza de gameplay e diferenciação instantânea entre personagens, inimigos e itens em todas as fases do seu projeto stealth.
