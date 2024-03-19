<script lang="ts">
  import { deleteTranslation } from "$lib/services/translation-service";
  import { translationsStore } from "$lib/stores";

  export let translationId: number;

  async function deleteTranslationClick() {
    await deleteTranslation(translationId);
    translationsStore.update(translations => translations.filter(t => t.id !== translationId));
  }
</script>

<button on:click={deleteTranslationClick}>
  <i class="fa fa-trash"></i>
</button>
