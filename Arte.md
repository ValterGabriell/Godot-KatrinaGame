

```markdown
# Guia de Paleta de Cores e Design Visual – Projeto Stealth

Documentação de design da paleta de cores para garantir consistência visual e clareza de gameplay (Stealth). A paleta é limitada a 8 cores para máximo contraste e comunicação visual eficiente.

---

## 1. Paleta de Cores Base (8 Cores Fixas)

A paleta foi desenhada para garantir **máximo contraste entre personagens e ambientes**, essencial para a clareza visual e o gameplay de stealth.

| Nome da Cor | Código HEX | Aplicação Primária |
| :--- | :--- | :--- |
| **Preto Profundo** | `#1A1A1A` | Sombras, Outlines, Áreas Ocultas, Dutos |
| **Cinza Escuro (Sombra)** | `#3D3D3D` | Paredes, Chão, *Stealth Zones* Seguras |
| **Cinza Claro (Metal)** | `#7A7A7A` | Grades, Portas, Itens Metálicos |
| **Amarelo Prisão** | `#E5D68A` | Luzes, Áreas de Risco, *Highlights* em Itens (Luz) |
| **Verde Ácido (Detalhe)** | `#6BAA75` | Terminais, Telas, Olhos de Myha |
| **Laranja Alerta** | `#E85D3C` | Alarmes, Estados de Alerta, Botões Perigosos |
| **Branco Sujo** | `#E8E8D0` | Pelagem de Inimigos, Reflexos, Papel, Comida |
| **Roxo Escuro (Opcional)** | `#4A3A5C` | Uniformes de Patrulheiros, Detalhes *High-Tech* |

---

## 2. Personagens & Hierarquia Visual

### 2.1. Protagonistas - Família Verde/Cinza

| Personagem | Cores Principais | Identificação Instantânea |
| :--- | :--- | :--- |
| **Myha** | Cinza Escuro + Olhos Verde Ácido | O verde é único e essencial para o estado de "stealth" (apenas os olhos brilham). Silhueta: orelhas triangulares. |
| **Katrina** | Cinza Claro + Olhos Amarelo Prisão | Inversão de paleta de Myha (cores mais claras). Silhueta: orelhas arredondadas. |

### 2.2. Inimigos - Hierarquia por Cores

A complexidade e a cor indicam a hierarquia de ameaça.

| Inimigo | Cores Principais | Nível de Ameaça Visual |
| :--- | :--- | :--- |
| **Gato Básico** | Branco Sujo + Olhos Laranja Alerta | Comum e menos ameaçador. |
| **Patrulheiro** | Branco Sujo + Uniforme Roxo Escuro | Unidade de Elite Visual (o roxo indica maior patente). |
| **Vigia** | Cinza Claro + Equipamentos Branco Sujo | Torre Tecnológica. |
| **Líder/Boss** | Roxo Escuro Dominante + Laranja Alerta | Único, intimidante, com a paleta mais complexa. |

---

## 3. Princípios de Design Aplicados

* **Silhuetas Distintivas:** Formato único para cada personagem (Ex: Myha – orelhas pontudas; Inimigos – formatos militares).
* **Comunicação de Estado:**
    * **Myha nas Sombras:** Apenas *outline* sutil + Olhos **Verde Ácido** brilhando.
    * **Inimigos Alertas:** Olhos piscando (**Laranja Alerta**), partículas de cor.
* **Iluminação como Gameplay:** O sistema Luz/Sombra é comunicado pela paleta.
    * Áreas **Amarelo Prisão** = Perigo / Risco.
    * Áreas **Cinza Escuro/Preto Profundo** = Segurança / *Stealth Zone*.

---

## 4. Planejamento das Fases (Progressão e Atmosfera)

Cada uma das 7 fases adapta a paleta de 8 cores para transmitir atmosfera e progressão.

**Regra:** Não usar mais de 4 cores simultâneas em um único *sprite*/item para manter a clareza.

| Fase | Atmosfera e Cores Dominantes | Destaques de Gameplay | Itens Essenciais (Cor) |
| :--- | :--- | :--- | :--- |
| **1: Tutorial/Prologue** | Preto, Cinza Escuro (Opressora) | Luz **Amarelo Prisão** apenas em áreas de perigo. | Chave (Cinza Claro), Terminal (Verde Ácido) |

<img width="1024" height="1024" alt="Gemini_Generated_Image_fviigwfviigwfvii" src="https://github.com/user-attachments/assets/849bd4a4-2f74-4589-ad9e-2cba5ff0e740" />


| **2: Bloco de Celas** | Cinza, Amarelo, Branco | Uniformes inimigos **Branco Sujo** se destacam. | Armário (Cinza Claro), Crachá (Laranja Alerta) |
<img width="1024" height="1024" alt="Gemini_Generated_Image_e0oyoae0oyoae0oy" src="https://github.com/user-attachments/assets/e6ab8b23-8efc-47d6-94c1-a3e8820e4693" />


| **3: Cozinha/Refeitório** | Cinza Claro, Branco Sujo | Itens de distração brilham (Amarelo/Verde). | Bandeja (Cinza Claro), Comida (Branco Sujo) |
<img width="1024" height="1024" alt="Gemini_Generated_Image_t31g76t31g76t31g" src="https://github.com/user-attachments/assets/a19cdd75-6be9-46e9-b44f-f6cd57e3c0aa" />

| **4: Área de Manutenção** | Cinza Escuro, Roxo Escuro | Introdução do **Roxo Escuro** nos Patrulheiros. Dutos **Preto Profundo** criam rotas seguras. | Ferramenta (Cinza Claro), Duto (Preto Profundo) |
<img width="1024" height="1024" alt="Gemini_Generated_Image_h8369bh8369bh836" src="https://github.com/user-attachments/assets/69fb458f-d4d5-4653-aa94-08c0584b255d" />

| **5: Sala de Controle/Fu<img width="1024" height="1024" alt="Gemini_Generated_Image_uz12o4uz12o4uz12" src="https://github.com/user-attachments/assets/ee2bea2e-0f22-4d7b-aa7c-e99354ba6177" />
ga** | Roxo Escuro, Laranja Alerta, Verde | Boss com paleta única. **Laranja Alerta** indica perigo máximo. | Painel (Verde Ácido), Alavanca (Laranja Alerta) |
![Uploading Gemini_Generated_Image_uz12o4uz12o4uz12.png…]()


```
