<script lang="ts">
  import { setContext } from "svelte";
  import { TestService } from "$lib/tests/test-service.svelte";

  const { children } = $props();

  const testService = new TestService();
  setContext("testService", testService);
</script>

{@render children()}
