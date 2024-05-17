<script lang="ts">
  import { TranslationService } from "$lib/translations/translation-service";

  let { translationId } = $props();

  const translationService = new TranslationService();

  async function deleteTranslationClick() {
    await translationService.deleteTranslation(translationId);
  }
</script>

<button on:click={deleteTranslationClick}>
  <i class="fa fa-trash"></i>
</button>
