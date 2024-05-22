<script lang="ts">
  import { TranslationService } from "$lib/translations/translation-service.svelte";
  import { setContext } from "svelte";

  const { children } = $props();

  const translationService = new TranslationService();
  setContext("translationService", translationService);
</script>

{@render children()}
