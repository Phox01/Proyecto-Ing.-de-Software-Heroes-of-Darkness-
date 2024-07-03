using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;

public class InteractBuy : MonoBehaviour
{
    public InventoryController Inventario;
    public TMP_Text texto;
    [SerializeField] private ItemSO Item;
    [SerializeField] private LocalizedString localizedBuyText;
    private bool Chocando = false;
    private SpriteRenderer spriteRenderer;
    private string localizedItemName;

    private void Start()
    {
        Inventario = FindObjectOfType<InventoryController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && Item != null)
        {
            spriteRenderer.sprite = Item.ItemImage;
            //AdjustSpriteSize(spriteRenderer, Item.ItemImage);
        }

        // Localize the item name first
        LocalizedString localizedItemNameString = new LocalizedString
        {
            TableReference = "Objetos",
            TableEntryReference = Item.Name 
        };

        localizedItemNameString.StringChanged += (localizedText) =>
        {
            localizedItemName = localizedText;
        };
    }

    private void Update()
    {
        Comprar(Inventario);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Chocando = true;
            UpdateLocalizedText();
            texto.gameObject.SetActive(true);
        }
        Debug.Log(Inventario.initialItems[0].quantity);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        texto.gameObject.SetActive(false);
        Chocando = false;
    }

    public void Comprar(InventoryController player)
    {
        if (Chocando == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if(DataJuego.data.dinero>=Item.Price){texto.gameObject.SetActive(false);
              
               
                DataJuego.data.dinero -=Item.Price;
                Inventario.inventoryData.AddItem(Item,1);
                Debug.Log(DataJuego.data.dinero);}
                else{
                    Debug.Log("Pobre");
                }

            }
        }
    }

    // private void AdjustSpriteSize(SpriteRenderer spriteRenderer, Sprite sprite)
    // {
    //     if (sprite == null)
    //         return;

    //     // Get the sprite's size in world units
    //     Vector2 spriteSize = sprite.bounds.size;

    //     // Adjust the object's scale to match the sprite size
    //     transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);
    // }

    private void UpdateLocalizedText()
    {
        localizedBuyText.Arguments = new object[] { localizedItemName, Item.Price };
        localizedBuyText.StringChanged += UpdateTextComponent;
    }

    private void UpdateTextComponent(string localizedText)
    {
        texto.text = localizedText;
    }

    private void OnDestroy()
    {
        localizedBuyText.StringChanged -= UpdateTextComponent;
    }
}
